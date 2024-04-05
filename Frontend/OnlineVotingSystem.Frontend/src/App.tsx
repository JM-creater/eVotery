import React from 'react'
import './App.css'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import Login from './components/Login'
import Register from './components/Register'
import Admin_Main from './components/admin/Admin_Main'
import Forgot_Password from './components/Forgot_Password'
import 'react-toastify/dist/ReactToastify.css';
import { ToastContainer, Zoom } from 'react-toastify'
import Reset_Password from './components/Reset_Password'
import Not_FoundPage from './components/common/Not_FoundPage'
import Voter_MainPage from './components/voter/Voter_MainPage'

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
          
          {/* Main */}

          <Route index element={ <Login/> } />
          <Route path='/register-page' element={ <Register/> } />
          <Route path='/reset-password' element={ <Reset_Password/> } />
          <Route path='/forgot-password' element={ <Forgot_Password/> } />

          {/* User */}

          <Route path='/home-page' element={ <Voter_MainPage/> } />

          {/* Admin */}

          <Route path='/admin-main' element={ <Admin_Main/> } />

          {/* Error */}
          <Route path="*" element={<Not_FoundPage />} />

        </Routes>
      </BrowserRouter>

    </React.Fragment>
  )
}

export default App
