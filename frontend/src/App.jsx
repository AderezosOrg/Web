import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import Button from './components/Button'
import { Formik, Form } from 'formik';
import InputField from './components/Input';


function App() {
  const [count, setCount] = useState(0);
  const [isNameCorrect, setIsNameCorrect] = useState(true);

  return (
    <>
      <div className="flex justify-center gap-4 py-8">
        <a href="https://vitejs.dev" target="_blank" rel="noopener noreferrer">
          <img src={viteLogo} className="h-24 hover:drop-shadow-[0_0_2em_#646cffaa]" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank" rel="noopener noreferrer">
          <img src={reactLogo} className="h-24 hover:drop-shadow-[0_0_2em_#61dafbaa]" alt="React logo" />
        </a>
      </div>
      <h1 className="text-5xl font-bold mb-6">Vite + React + Tailwind</h1>

      <Formik
        initialValues={{ name: ''}}
        onSubmit={(values) => {
          if (!values.name.trim())
            {
              setIsNameCorrect(false);
              return;
            } else {
              setIsNameCorrect(true);
            }
          console.log('Submitted values:', values);
        }}
      >
        {() => (
          <Form className="flex flex-col gap-4">
            <InputField
              id="name"
              name="name"
              placeholder="Name..."
              label="Name"
              isCorrect={isNameCorrect}
            />
          </Form>
        )}
      </Formik>

      <div className="card p-6">
        <Button
          type={"common"}
          onClick={() => setCount((count) => count + 1)}
        >
          Count is {count}
        </Button>
        <p className="mt-4">
          Edita <code>src/App.jsx</code> y guarda para ver el cambio.
        </p>
      </div>
      <p className="mt-8 text-gray-500">
        Click en los logos de Vite y React para aprender más.
      </p>
    </>
  )
}

export default App
