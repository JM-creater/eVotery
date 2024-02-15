import { Button, Checkbox, Form, Input } from 'antd'
import React from 'react'
import '../components/Login.css'
import { LockOutlined, UserOutlined } from '@ant-design/icons';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import { toast } from 'react-toastify';

const LOGIN_URL = 'https://localhost:7196/Voter/login';

type VoterType = {
    voterIdOrEmail?: string;
    password?: string;
};

type LoginType = {
    password?: string;
    email?: string;
    voterId?: number;
}

const onFinishFailed = (errorInfo: unknown) => {
    console.log('Failed:', errorInfo);
};

const Login: React.FC = () => {

    const navigate = useNavigate();

    const delay = (ms: number | undefined) => new Promise(resolve => setTimeout(resolve, ms));

    const handleLogin = async (values: VoterType) => {

        const loginRequest: LoginType = {
            password: values.password,
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

            if (response.data.responseCode === 200) {
                toast.success('Login successful.');
                await delay(2000);
                navigate('/home-page');
            } else if (response.data.responseCode === 400) {
                toast.error('Login failed. Incorrect username or password.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }
    
        } catch (error) {
            console.error(error);
        }
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
            >
                <h1 className="title-label">
                    eVotery - Online Voting System
                </h1>

                <Form.Item<VoterType>
                    name="voterIdOrEmail"
                    rules={[{ required: true, message: 'Please input your Voter ID or Email.' }]}
                >
                    <Input prefix={<UserOutlined className="site-form-item-icon" />} placeholder="Voter's ID or Email" />
                </Form.Item>
                
                <Form.Item<VoterType>
                    name="password"
                    rules={[{ required: true, message: 'Please input your Password.' }]}
                >
                    <Input
                        prefix={<LockOutlined className="site-form-item-icon" />}
                        type="password"
                        placeholder="Password"
                    />
                </Form.Item>

                <Form.Item>
                    <div className="form-remember-forgot">
                        <Form.Item noStyle>
                            <Checkbox>Remember me</Checkbox>
                        </Form.Item>

                        <Link to={'/forgot-password'}>
                            <a className="login-form-forgot">
                                Forgot password
                            </a>
                        </Link>
                    </div>
                </Form.Item>

                <Form.Item>
                    <div className="login-register-container">
                        <div className="login-button-container">
                            <Button type="primary" htmlType="submit" className="login-form-button">
                                Log in
                            </Button>
                        </div>
                        
                        <div className="register-button-container">
                            <span>No account?</span>
                            
                            <Link to={'/register'}>
                                <a className='register-title'>Register now!</a>
                            </Link>
                        </div>
                    </div>
                </Form.Item>

            </Form>
        </div>
    );
    
}

export default Login