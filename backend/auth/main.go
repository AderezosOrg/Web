package main

import (
	"fmt"
	"html/template"
	"encoding/json"
	"log"
	"net/http"
	"io/ioutil"
	"os"
	"sort"
	"bytes"

	"github.com/subosito/gotenv"
	"github.com/gorilla/sessions"
	"github.com/gorilla/pat"
	"github.com/markbates/goth"
	"github.com/markbates/goth/gothic"

	"github.com/markbates/goth/providers/google"
	"github.com/markbates/goth/providers/discord"
	"github.com/markbates/goth/providers/microsoftonline"
)

const(
	redirectionFrontEndRootUrl = "http://localhost:5173/"
	redirectionBackendPostSessionUrl = "http://localhost:5009/api/Session/"
	redirectionBackendGetCookieUrl = "http://localhost:5009/api/Session/cookie/"
	key = "randomString"
	MaxAge = 86400 * 30
	IsProd = false
)

func main() {
	gotenv.Load()

	store := sessions.NewCookieStore([]byte(key))
	store.Options.Path = "/"
	store.Options.HttpOnly = true

	gothic.Store = store

	goth.UseProviders(
		google.New(os.Getenv("GOOGLE_KEY"), os.Getenv("GOOGLE_SECRET"), "http://localhost:8000/auth/google/callback"),
		discord.New(os.Getenv("DISCORD_KEY"), os.Getenv("DISCORD_SECRET"), "http://localhost:8000/auth/discord/callback", discord.ScopeIdentify, discord.ScopeEmail),
		microsoftonline.New(os.Getenv("MICROSOFTONLINE_KEY"), os.Getenv("MICROSOFTONLINE_SECRET"), "http://localhost:8000/auth/microsoftonline/callback"),
	)


	m := map[string]string{
		"google":          "Google",
		"discord":         "Discord",
		"microsoftonline": "Microsoft Online",
		}

	var keys []string
	for k := range m {
		keys = append(keys, k)
	}
	sort.Strings(keys)

	providerIndex := &ProviderIndex{Providers: keys, ProvidersMap: m}

	p := pat.New()
	p.Get("/auth/{provider}/callback", func(res http.ResponseWriter, req *http.Request) {
		user, err := gothic.CompleteUserAuth(res, req)
		if err != nil {
			res.Header().Set("Location", redirectionFrontEndRootUrl)
			res.WriteHeader(http.StatusTemporaryRedirect)
		}

		userEmail := user.Email
		userToken := user.AccessToken
	
		var jsonStr = []byte("{\"email\" : \""+userEmail+"\",\"token\"	: \""+userToken+"\"}")
		jreq, jerr := http.NewRequest("POST",redirectionBackendPostSessionUrl, bytes.NewBuffer(jsonStr))
		jreq.Header.Set("Content-Type","application/json")

		client := &http.Client{}
		hresp, jerr := client.Do(jreq)
		if jerr != nil {
			return
		}

		defer hresp.Body.Close()

		var r PostCookieRequest

		requestBody, err := ioutil.ReadAll(hresp.Body)

		err = json.Unmarshal(requestBody, &r)
		if err != nil {
			fmt.Println(err)
			return
		}
		
		res.Header().Set("Location", redirectionBackendGetCookieUrl+r.SessionID)
		res.WriteHeader(http.StatusTemporaryRedirect)
	})

	p.Get("/logout/{provider}", func(res http.ResponseWriter, req *http.Request) {
		gothic.Logout(res, req)
		res.Header().Set("Location", "http://localhost:8000")
		res.WriteHeader(http.StatusTemporaryRedirect)
	})

	p.Get("/auth/{provider}", func(res http.ResponseWriter, req *http.Request) {
		gothic.BeginAuthHandler(res, req)
	})

	p.Get("/", func(res http.ResponseWriter, req *http.Request) {
		t, _ := template.New("foo").Parse(indexTemplate)
		t.Execute(res, providerIndex)
	})

	log.Println("listening on localhost:8000")
	log.Fatal(http.ListenAndServe(":8000", p))
}

type PostCookieRequest struct {
	SessionID   string `json:"sessionID"`
	Token       string `json:"token"`
	ContactID   string `json:"contactID"`
	Email       string `json:"email"`
	PhoneNumber string `json:"phoneNumber"`
}

type ProviderIndex struct {
	Providers    []string
	ProvidersMap map[string]string
}

var indexTemplate = `{{range $key,$value:=.Providers}}
    <p><a href="/auth/{{$value}}">Log in with {{index $.ProvidersMap $value}}</a></p>
{{end}}`

var userTemplate = `
<p><a href="/logout/{{.Provider}}">logout</a></p>
<p>Name: {{.Name}} [{{.LastName}}, {{.FirstName}}]</p>
<p>Email: {{.Email}}</p>
<p>NickName: {{.NickName}}</p>
<p>Location: {{.Location}}</p>
<p>AvatarURL: {{.AvatarURL}} <img src="{{.AvatarURL}}"></p>
<p>Description: {{.Description}}</p>
<p>UserID: {{.UserID}}</p>
<p>AccessToken: {{.AccessToken}}</p>
<p>ExpiresAt: {{.ExpiresAt}}</p>
<p>RefreshToken: {{.RefreshToken}}</p>
`