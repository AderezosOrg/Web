import Header from "./layouts/Header";
import { Routes, Route } from 'react-router-dom';

import './index.css';
import EmptyPage from "./pages/EmptyPage";

function App() {
  return (
    <div className="flex flex-col h-screen w-full">
      <Header/>
      <Routes>
        <Route path='/' element={<EmptyPage/>} />
      </Routes>
    </div>
  );
}

export default App;
