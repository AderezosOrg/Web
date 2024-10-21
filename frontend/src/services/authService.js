import { jwtDecode } from 'jwt-decode';

export function getCookie(name) {
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);
  if (parts.length === 2) {
    const cookie = parts.pop().split(';').shift();
    console.log(`Cookie "${name}":`, cookie);
    return cookie;
  }
  console.warn(`Cookie "${name}" not found.`);
  return null;
}

export function getDecodedToken() {
  const token = getCookie('session');
  if (token) {
    try {
      const decoded = jwtDecode(token);
      console.log("Decoded Token:", decoded);
      return decoded;
    } catch (error) {
      console.error('Error decoding token:', error);
      return null;
    }
  } else {
    console.warn("Token not found in cookies.");
  }
  return null;
}
