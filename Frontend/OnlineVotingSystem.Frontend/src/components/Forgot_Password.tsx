import { Alert, Button, Form, Input } from 'antd'
import Logo from '../assets/samples/Logo.png';
import ForgotPasswordImage from '../assets/samples/forgot_password_image.svg'
import React, { useEffect, useState } from 'react'
import '../components/Forgot_Password.css'
import { LockOutlined } from '@ant-design/icons';
import { useLocation, useNavigate } from 'react-router-dom';
import axios from 'axios';
import { toast } from 'react-toastify';

const RESETPASSWORD_URL = 'https://localhost:7196/User/reset-password'
const VALIDATETOKEN_URL = 'https://localhost:7196/User/validate-reset-token?token='

type ResetType = {
    token?: string;
    newPassword?: string;
    confirmPassword?: string;
}

const onFinishFailed = (errorInfo: unknown) => {
    console.log('Failed:', errorInfo);
};

const Forgot_Password: React.FC = () => {

    const [loadings, setLoadings] = useState<boolean>(false);
    const [resetToken, setResetToken] = useState<string>("");
    const location = useLocation();
    const navigate = useNavigate();

    useEffect(() => {
        const token = new URLSearchParams(location.search).get("token");
        
        if (!token) {
            navigate("/");
        } else {
            axios.get(`${VALIDATETOKEN_URL}${token}`)
            .then(response => {
                if(response.data.isValid) {
                    localStorage.setItem("token", token);
                    setResetToken(token);
                } else {
                    navigate("/");
                }
            })
            .catch(error => {
                console.error(error);
                navigate("/");
            });
        }
    }, [location, navigate]);

    const handleForgotPassword = async (values: ResetType) => {
        setLoadings(true);
        
        if (values.newPassword !== values.confirmPassword) {
            toast.error('Passwords do not match.');
            setLoadings(false);
            return;
        }
    
        const forgotPasswordRequest: ResetType = {
            token: resetToken,  
            newPassword: values.newPassword, 
        };
    
        try {
            const response = await axios.put(RESETPASSWORD_URL, forgotPasswordRequest, {
                headers: {
                    'Content-Type': 'application/json',
                },
            });
    
            if (response.data.responseCode === 200) {
                localStorage.removeItem('token');
                toast.success("Successfully updated the password");
                navigate("/");
            } else if (response.data.responseCode === 400) {
                toast.error('Password reset failed.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }
    
        } catch (error) {
            console.error(error);
        } finally {
            setLoadings(false);
        }
    };
    

    return (
        <div className="forgot-main-container">
            <Form
                initialValues={{ remember: true }}
                onFinish={handleForgotPassword}
                onFinishFailed={onFinishFailed}
                autoComplete="off"
            >
                <div className='main-forgot'>
                    <div className="forgot-content-left">
                        <img src={ForgotPasswordImage} alt="forgot-password-image" className='forgot-image' />
                    </div>

                    <div className="forgot-content-right">
                        <div className="reset-image-container">
                            <img src={Logo} alt="eVotery-logo" />
                        </div>

                        <h1 className="reset-title-label">
                            Reset Password
                        </h1>
                        
                        <div className="information-container">
                            <Alert
                                description="Enter your new password and confirm it below to reset your password."
                            />
                        </div>
                        
                        <label>New Password:</label>
                        <Form.Item<ResetType>
                            name="newPassword"
                            rules={[{ required: true, message: 'Please input your new password.' }]}
                        >
                            <Input 
                                type='password' 
                                prefix={<LockOutlined />} 
                                placeholder="Enter your password"  
                            />
                        </Form.Item>

                        <label>Confirm Password:</label>
                        <Form.Item<ResetType>
                            name="confirmPassword"
                            dependencies={['newPassword']}
                            rules={[
                                { required: true, message: 'Please confirm your password.' },
                                ({ getFieldValue }) => ({
                                    validator(_, value) {
                                        if (!value || getFieldValue('newPassword') === value) {
                                            return Promise.resolve();
                                        }
                                        return Promise.reject(new Error('The two passwords that you entered do not match.'));
                                    },
                                }),
                            ]}
                        >
                            <Input 
                                type='password' 
                                prefix={<LockOutlined />} 
                                placeholder="Confirm your password" 
                            />
                        </Form.Item>

                        <Form.Item>
                            <div className="login-register-container">
                                <div className="login-button-container">
                                    <Button 
                                        type="primary" 
                                        htmlType="submit" 
                                        className="login-form-button"
                                        loading={loadings}
                                    >
                                        Confirm
                                    </Button>
                                </div>
                            </div>
                        </Form.Item>
                    </div>
                </div>
            </Form>
        </div>
    )
}

export default Forgot_Password