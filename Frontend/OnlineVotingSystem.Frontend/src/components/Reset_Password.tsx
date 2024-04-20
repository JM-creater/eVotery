import { Alert, Button, Form, Input } from 'antd'
import React, { useState } from 'react'
import '../components/Reset_Password.css'
import { MailOutlined } from '@ant-design/icons';
import { Link, useNavigate,  } from 'react-router-dom';
import Logo from '../assets/samples/Logo.png';
import axios from 'axios';
import { toast } from 'react-toastify';

const FORGOTPASSWORD_URL = 'https://localhost:7196/User/forgot-password?email='

type ResetType = {
    email?: string;
}

const onFinishFailed = (errorInfo: unknown) => {
    console.log('Failed:', errorInfo);
};

const Reset_Password: React.FC = () => {
    
    const navigate = useNavigate();
    const [loadings, setLoadings] = useState<boolean>(false);

    const handleResetPassword = async (values: ResetType) => {
        setLoadings(true);

        try {
            const response = await axios.put(`${FORGOTPASSWORD_URL}${values.email}`, {
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            if (response.data.responseCode === 200) {
                navigate('/');
                localStorage.removeItem('result');
                localStorage.removeItem('userId');
            } else if (response.data.responseCode === 400) {
                toast.error('Email not yet registered.');
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
        
        <div className="reset-main-container">
            <Form
                className="reset-form-container"
                style={{ maxWidth: 500 }}
                initialValues={{ remember: true }}
                onFinish={handleResetPassword}
                onFinishFailed={onFinishFailed}
                autoComplete="off"
            >
                
                <div className="reset-image-container">
                    <img src={Logo} alt="eVotery-logo" />
                </div>

                <h1 className="reset-title-label">
                    Trouble Logging In?
                </h1>
                
                <div className="information-container">
                    <Alert
                        description="Enter your email and we'll send you a link to get back into your account."
                    />
                </div>

                <Form.Item<ResetType>
                    name="email"
                    rules={[{ required: true, message: 'Please input your valid email.' }]}
                >
                    <Input 
                        size='large'
                        maxLength={30} 
                        prefix={<MailOutlined className="site-form-item-icon" />} 
                        placeholder="Enter your valid email" 
                    />
                </Form.Item>

                <Form.Item>
                    <div className="reset-container">
                        <div className="reset-button-container">
                            <Button 
                                size='large'
                                type="primary" 
                                htmlType="submit" 
                                className="reset-form-button"
                                loading={loadings}
                            >
                                Reset Password
                            </Button>
                        </div>
                        
                        <div className="goback-button-container">
                            <span className='title-no-account'>Go back.</span>
                            
                            <Link to={'/'}>
                                <a className='reset-title'>Login</a>
                            </Link>
                        </div>
                    </div>
                </Form.Item>

            </Form>
        </div>
    );
    
}

export default Reset_Password