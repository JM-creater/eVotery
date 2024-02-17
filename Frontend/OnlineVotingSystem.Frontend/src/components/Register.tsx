import React, { useState } from 'react'
import {
    Button, 
    DatePicker, 
    Form, 
    Input, 
    Radio, 
    Upload, 
    UploadProps, 
    message 
} from "antd";
import { 
    ArrowLeftOutlined,
    HomeOutlined, 
    LockOutlined, 
    MailOutlined, 
    PhoneOutlined, 
    UploadOutlined, 
    UserOutlined 
} from '@ant-design/icons';
import Logo from '../assets/samples/Logo.png';
import '../components/Register.css'
import axios from 'axios';
import { toast } from 'react-toastify';
import { Link, useNavigate } from 'react-router-dom';

const REGISTER_URL = 'https://localhost:7196/Voter/create';

type VoterType = {
    firstName?: string;
    lastName?: string;
    dateOfBirth?: string;
    email?: string;
    password?: string;
    confirmPassword?: string;
    address?: string;
    phoneNumber?: string;
    gender?: string;
    voterImages?: FileType[];
};

type FileType = {
    uid: string;
    name: string;
    status: string;
    originFileObj: File;
}


const props: UploadProps = {
    name: 'voterImages',
    headers: {
        'Content-Type': 'multipart/form-data',
    },

    beforeUpload: () => false,

    onChange(info) {
        if (info.file.status === 'done') {
            message.success(`${info.file.name} file uploaded successfully`);
        } else if (info.file.status === 'error') {
            message.error(`${info.file.name} file upload failed.`);
        }
    },
};

const Register: React.FC = () => {

    const [loadings, setLoadings] = useState<boolean>(false);
    const navigate = useNavigate();

    const delay = (ms: number | undefined) => new Promise(resolve => setTimeout(resolve, ms));

    const handleRegister = async (values: VoterType) => {
        setLoadings(true);

        try {

            const formData = new FormData();

            Object.entries(values).forEach(([key, value]) => {
                if (key !== 'voterImages' && value !== undefined) {
                    formData.append(key, value.toString());
                }
            });

            if (values.voterImages && values.voterImages.length > 0) {
                const file = values.voterImages[0].originFileObj;
                if (file) {
                    formData.append('voterImages', file);
                }
            }

            if (values.password !== values.confirmPassword) {
                toast.error('Password do not match.')
            }

            const response = await axios.post(REGISTER_URL, formData, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            })

            if (response.data.responseCode === 200) {
                toast.success('Successfully Created an Account.');
                await delay(2000);
                navigate('/');
            } else if (response.data.responseCode === 400) {
                toast.error('Register failed. Please check your information.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }

        } catch (error) {
            console.log(error);
        } finally {
            setLoadings(false);
        }
    };

    const onFinishFailed = (errorInfo: unknown) => {
        console.log('Failed:', errorInfo);
    };

    return (
        <div className="register-main-container">
            <Form
                className="register-form-container"
                initialValues={{ remember: true }}
                onFinish={handleRegister}
                onFinishFailed={onFinishFailed}
                autoComplete="off"
            >
                <Link to={'/'}>
                    <ArrowLeftOutlined /> Back
                </Link> 

                <div className="image-register-container">
                    <img src={Logo} alt="eVotery-logo" />
                </div>

                <h1 className="register-title-label">
                    REGISTER
                </h1>

                <div className='input-grid-container'>

                <Form.Item<VoterType>
                    label="Image"
                    name="voterImages"
                    rules={[{ 
                        required: true, message: 'Please upload an image.' 
                    }]}
                    valuePropName="fileList"
                    getValueFromEvent={(e) => {
                        if (Array.isArray(e)) {
                            return e;
                        } 
                        return e && e.fileList;
                    }}
                >
                    <Upload {...props} listType="picture">
                        <Button icon={<UploadOutlined/>}>Upload Image</Button>
                    </Upload>
                </Form.Item>

                    <Form.Item<VoterType>
                        label="First Name"
                        name="firstName"
                        rules={[{ required: true, message: 'Enter your First Name.' }]}
                    >
                        <Input
                            maxLength={15}
                            prefix={<UserOutlined className="site-form-item-icon" />}
                            placeholder="First Name"
                        />
                    </Form.Item>

                    <Form.Item<VoterType>
                        label="Last Name"
                        name="lastName"
                        rules={[{ required: true, message: 'Enter your Last Name.' }]}
                    >
                        <Input
                            maxLength={15}
                            prefix={<UserOutlined className="site-form-item-icon" />}
                            placeholder="Last Name"
                        />
                    </Form.Item>

                    <Form.Item<VoterType>
                        label="Email"
                        name="email"
                        rules={[{ required: true, message: 'Enter your Email.' }]}
                    > 
                        <Input
                            maxLength={30}
                            prefix={<MailOutlined className="site-form-item-icon" />}
                            type="email"
                            placeholder="Email"
                        />
                    </Form.Item>

                    <Form.Item<VoterType>
                        label="Date of Birth"
                        name="dateOfBirth"
                        rules={[{ required: true, message: 'Enter your Date of Birth.' }]}
                    > 
                        <DatePicker format="MM-DD-YYYY" />
                    </Form.Item>

                    <Form.Item<VoterType>
                        label="Address"
                        name="address"
                        rules={[{ required: true, message: 'Enter your Address.' }]}
                    > 
                        <Input
                            maxLength={30}
                            prefix={<HomeOutlined className="site-form-item-icon" />}
                            placeholder="Address"
                        />
                    </Form.Item>

                    <Form.Item<VoterType>
                        label="Phone Number"
                        name="phoneNumber"
                        rules={[{ required: true, message: 'Enter your Phone Number.' }]}
                    > 
                        <Input
                            maxLength={11}
                            prefix={<PhoneOutlined className="site-form-item-icon" />}
                            placeholder="Phone Number"
                        />
                    </Form.Item>

                    <Form.Item<VoterType>
                        label="Gender"
                        name="gender"
                        rules={[{ required: true, message: 'Select your gender.' }]}
                    >
                        <Radio.Group>
                            <Radio value="1"> Female </Radio>
                            <Radio value="2"> Male </Radio>
                        </Radio.Group>
                    </Form.Item>

                    <Form.Item<VoterType>
                        label="Password"
                        name="password"
                        rules={[{ required: true, message: 'Enter your Password.' }]}
                    >
                        <Input
                            maxLength={8}
                            prefix={<LockOutlined className="site-form-item-icon" />}
                            type="password"
                            placeholder="Password"
                        />
                    </Form.Item>

                    <Form.Item<VoterType>
                        label="Confirm Password"
                        name="confirmPassword"
                        rules={[{ required: true, message: 'Confirm your password.' }]}
                    >
                        <Input
                            maxLength={8}
                            prefix={<LockOutlined className="site-form-item-icon" />}
                            type="password"
                            placeholder="Confirm Password"
                        />
                    </Form.Item>
                    
                </div>


                <Form.Item>
                    <div className="register-voter-container">
                        <Button 
                            type="primary" 
                            htmlType="submit" 
                            className="register-form-button"
                            loading={loadings}
                        >
                            Register
                        </Button>
                    </div>
                </Form.Item>

            </Form>
        </div>
    );
}

export default Register