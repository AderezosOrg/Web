import Header from "./layouts/Header";
import { Routes, Route } from 'react-router-dom';
import DateForm from "./pages/DateForm";
import NotFound from "./pages/NotFound";
import Confirmation from "./pages/Confirmation";

function App() {
  return (
    <div className="flex flex-col h-screen w-full">
      <Header/>
      <Routes>
      <Route path='*' element={<NotFound />} />
        <Route path='/' element={<DateForm/>} />
        <Route path="/confirmation" element={<Confirmation />} />
      </Routes>
    </div>
  );
}

export default App;
