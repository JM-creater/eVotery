import React from 'react'
import './App.css'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import Login from './components/Login'
import Register from './components/Register'
import HomePage from './components/voter/HomePage'
import Admin_Main from './components/admin/Admin_Main'
import Forgot_Password from './components/Forgot_Password'
import 'react-toastify/dist/ReactToastify.css';
import { ToastContainer, Zoom } from 'react-toastify'
import Reset_Password from './components/Reset_Password'

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
          <Route path='/register-page' element={ <Register/> } />
          <Route path='/reset-password' element={ <Reset_Password/> } />
          <Route path='/forgot-password' element={ <Forgot_Password/> } />

          <Route path='/home-page' element={ <HomePage/> } />

          <Route path='/admin-main' element={ <Admin_Main/> } />

        </Routes>
      </BrowserRouter>

    </React.Fragment>
  )
}

export default App
