import React from 'react'
import './App.css'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import Login from './components/Login'
import Register from './components/Register'
import HomePage from './components/voter/HomePage'
import 'react-toastify/dist/ReactToastify.css';
import { ToastContainer, Zoom } from 'react-toastify'

function App() {

  return (
    <React.Fragment>
      <ToastContainer
        position="top-center"
        autoClose={1500}
        hideProgressBar={false}
        newestOnTop
        limit={1}
        transition={Zoom}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable={false}
        pauseOnHover={false}
        theme="light"
      />
      <BrowserRouter>
        <Routes>

          <Route index element={ <Login/> } />
          <Route path='/register' element={ <Register/> } />

          <Route path='/home-page' element={ <HomePage/> } />

        </Routes>
      </BrowserRouter>

    </React.Fragment>
  )
}

export default App
