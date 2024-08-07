import { Alert, Button, Checkbox, Flex, Form, Input } from 'antd'
import React, { useState } from 'react'
import '../components/Login.css'
import { EyeInvisibleOutlined, EyeTwoTone, LockOutlined, UserOutlined } from '@ant-design/icons';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import { toast } from 'react-toastify';
import Logo from '../assets/samples/Logo.png';
import { useAuth } from '../utils/AuthContext';
import ReCAPTCHA from "react-google-recaptcha";

const siteApiUrl = import.meta.env.SITE_URL_KEY;
const LOGIN_URL = 'https://localhost:7196/User/login';
const SITE_URL_KEY = siteApiUrl;

type VoterType = {
    voterIdOrEmail?: string;
    password?: string;
};

type LoginType = {
    password?: string;
    email?: string;
    voterId?: number;
    recaptchaToken: string;
}

const onFinishFailed = (errorInfo: unknown) => {
    console.log('Failed:', errorInfo);
};

const Login: React.FC = () => {
    
    const navigate = useNavigate();
    const [loadings, setLoadings] = useState<boolean>(false);
    const [errorField, setErrorField] = useState<string>("");
    const [recaptchaToken, setRecaptchaToken] = useState<string>("");
    const [form] = Form.useForm();
    const { login } = useAuth();

    const handleLogin = async (values: VoterType) => {
        setLoadings(true);

        const loginRequest: LoginType = {
            ...values,
            recaptchaToken: recaptchaToken, 
        };
    
        if (values.voterIdOrEmail?.includes('@')) {
            loginRequest.email = values.voterIdOrEmail;
        } else {
            const voterId = parseInt(values.voterIdOrEmail || '0', 10);
            if (!isNaN(voterId)) {
                loginRequest.voterId = voterId;
            }
        }
        
        try {
            const response = await axios.post(LOGIN_URL, loginRequest, {
                headers: {
                    'Content-Type': 'application/json',
                },
            });
            
            const resultJson = JSON.stringify(response.data.result);
            const tokenJson = response.data.token;
            const userIdJson = response.data.userId;

            localStorage.setItem('result', resultJson);
            localStorage.setItem('token', tokenJson);
            localStorage.setItem('userId', userIdJson);

            login(tokenJson);

            form.resetFields();

            if (response.data.responseCode === 200) {
                switch(response.data.userRole) {
                    case 1: 
                        navigate('/home-page');
                        break;
                    case 2: 
                        navigate('/admin-main');
                        break;
                    default:
                        console.log("Unknown role");
                        break;
                } 
            } else if (response.data.responseCode === 400 || response.data.responseCode === 500) {
                const errorMessage = response.data.errorMessage;
                setErrorField(errorMessage);
            } else {
                toast.error('An error occurred. Please try again later.');
            }
    
        } catch (error) {
            console.error(error);
        } finally {
            setLoadings(false);
        }
    };

    const onReCAPTCHAChange = (token: string | null) => {
        setRecaptchaToken(token || "");
    };

    return (
        
        <div className="login-main-container">
            <Form
                className="login-form-container"
                style={{ maxWidth: 500 }}
                initialValues={{ remember: true }}
                onFinish={handleLogin}
                onFinishFailed={onFinishFailed}
                autoComplete="off"
                form={form}
            >
                
                <div className="image-container">
                    <img src={Logo} alt="eVotery-logo" />
                </div>
                
                <h1 className="title-label">LOGIN</h1>

                {
                    errorField && (
                        <Alert 
                            className="alert-container"
                            message={errorField}
                            type="error"
                        />
                    )
                }

                <Form.Item<VoterType>
                    name="voterIdOrEmail"
                    rules={[{ 
                        required: true, 
                        message: 'Please input your Voter ID or Email.' 
                    }]}
                >
                    <Input 
                        size='large' 
                        prefix={<UserOutlined className="site-form-item-icon" />} 
                        placeholder="Voter's ID or Email" 
                    />
                </Form.Item>
                
                <Form.Item<VoterType>
                    name="password"
                    rules={[{ required: true, message: 'Please input your Password.' }]}
                >
                    <Input.Password
                        size='large'
                            prefix={<LockOutlined className="site-form-item-icon" />}
                            type="password"
                            placeholder="Password"
                            iconRender={(visible) => (visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />)}
                        />
                </Form.Item>

                <Flex justify='center' align='center' style={{ marginBottom: '8px' }}>
                    <ReCAPTCHA
                        sitekey={SITE_URL_KEY}
                        onChange={onReCAPTCHAChange}
                    />
                </Flex> 

                <Form.Item>
                    <div className="form-remember-forgot">
                        <Form.Item noStyle>
                            <Checkbox className='title-remember'>Remember me</Checkbox>
                        </Form.Item>

                        <Link to={'/reset-password'}>
                            <div className="login-form-forgot">
                                Forgot password
                            </div>
                        </Link>
                    </div>
                </Form.Item>

                <Form.Item>
                    <div className="login-register-container">
                        <div className="login-button-container">
                            <Button 
                                size='large'
                                type="primary" 
                                htmlType="submit" 
                                className="login-form-button"
                                loading={loadings}
                            >
                                Log in
                            </Button>
                        </div>
                        
                        <div className="register-button-container">
                            <span className='title-no-account'>No account?</span>
                            
                            <Link to={'/register-page'}>
                                <div className='register-title'>Register now!</div>
                            </Link>
                        </div>
                    </div>
                </Form.Item>

            </Form>
        </div>
    );
    
}

export default Login