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
import { AuthProvider } from './utils/AuthProvider'
import PrivateRoute from './utils/private_route'
import Voter_MainPage from './components/voter/Voter_MainPage'
import PrivatePassRoute from './utils/private_passroute'

function App() {

  return (
    <AuthProvider>
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
            <Route path='/forgot-password' element={ <PrivatePassRoute> <Forgot_Password/> </PrivatePassRoute> } />

            <Route path='/home-page' element={ <PrivateRoute> <Voter_MainPage/> </PrivateRoute> } />

            <Route path='/admin-main' element={ <PrivateRoute> <Admin_Main/> </PrivateRoute> } />

            <Route path="*" element={<Not_FoundPage />} />

          </Routes>
        </BrowserRouter>

      </React.Fragment>
    </AuthProvider>
  )
}

export default App
