import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'

function App() {
  const [count, setCount] = useState(0)

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
      <div className="card p-6">
        <button
          onClick={() => setCount((count) => count + 1)}
          className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
        >
          Count is {count}
        </button>
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
