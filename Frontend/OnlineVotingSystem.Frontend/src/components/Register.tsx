import React, { useEffect, useState } from 'react'
import {
    Button, 
    DatePicker, 
    Typography,
    Form, 
    Input, 
    Radio, 
    Steps, 
    UploadProps, 
    message, 
    theme,
    Flex,
    Tooltip,
    Select,
    Checkbox
} from "antd";
import { 
    ArrowLeftOutlined,
    EyeInvisibleOutlined,
    EyeTwoTone,
    HomeOutlined, 
    InboxOutlined, 
    InfoCircleOutlined, 
    LockOutlined, 
    MailOutlined, 
    NumberOutlined, 
    PhoneOutlined, 
    SolutionOutlined,  
    UserOutlined 
} from '@ant-design/icons';
import Logo from '../assets/samples/Logo.png';
import '../components/Register.css'
import axios from 'axios';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import Dragger from 'antd/es/upload/Dragger';

const REGISTER_URL = 'https://localhost:7196/User/register';
const GETALL_DOCUMENTS_URL = 'https://localhost:7196/PersonalDocument/getall-documents';

type StepOneType = {
    firstName?: string;
    lastName?: string;
    suffixName?: string;
    dateOfBirth?: string;
    gender?: string;
}

type VoterType = {
    firstName?: string;
    lastName?: string;
    personalDocumentId?: string;
    nationality?: string;
    religion?: string;
    zipCode?: string;
    occupation?: string;
    dateOfBirth?: string;
    email?: string;
    password?: string;
    confirmPassword?: string;
    address?: string;
    phoneNumber?: string;
    gender?: string;
    voterImages?: FileType[];
    hasAgreedToTerms?: boolean;
    idNUmber?: string;
};

type FileType = {
    uid: string;
    name: string;
    status: string;
    originFileObj: File;
}

type DocumentType = {
    id?: string;
    document?: string;
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

const onFinishFailed = (errorInfo: unknown) => {
    console.log('Failed:', errorInfo);
};

const { Text  } = Typography;

const Register: React.FC = () => {

    const [loadings, setLoadings] = useState<boolean>(false);
    const navigate = useNavigate();
    const { token } = theme.useToken();
    const [current, setCurrent] = useState(0);
    const [personalDocuments, setPersonalDocuments] = useState<DocumentType[]>([]);

    const delay = (ms: number | undefined) => new Promise(resolve => setTimeout(resolve, ms));

    const contentStyle: React.CSSProperties = {
        lineHeight: '260px',
        textAlign: 'center',
        color: token.colorTextTertiary,
        backgroundColor: token.colorFillAlter,
        borderRadius: token.borderRadiusLG,
        border: `1px dashed ${token.colorBorder}`,
        marginTop: 16,
    };

    const handleBackLogin = () => {
        navigate('/');
    };

    const next = () => {
        if (current < steps.length - 1) {
            setCurrent(current + 1);
        }
    };
    
    const prev = () => {
        setCurrent(current - 1);
    };

    useEffect(() => {
        const fetchDocuments = async () => {
            try {
                const response = await axios.get(GETALL_DOCUMENTS_URL);
                setPersonalDocuments(response.data);
            } catch (error) {
                console.error(error);
            }
        }

        fetchDocuments();
    }, []);

    // const handleStepOneRegister = async (values: ) => {

    // };

    const handleRegister = async (values: VoterType) => {
        setLoadings(true);

        if (values.password !== values.confirmPassword) {
            toast.error('Passwords do not match.');
            setLoadings(false);
            return;
        }

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

    const renderFormContent = (currentStep: number) => {
        switch (currentStep) {
            case 0: 
                return (
                    <React.Fragment>
                        <Form
                            className="register-form-container"
                            initialValues={{ remember: true }}
                            onFinish={handleRegister}
                            onFinishFailed={onFinishFailed}
                            autoComplete="off"
                        >
                            <Button 
                                className='link-back-content' 
                                onClick={handleBackLogin}    
                            >
                                <ArrowLeftOutlined /> Back
                            </Button> 
            
                            <div className="image-register-container">
                                <img className="image-register-content" src={Logo} alt="eVotery-logo" />

                                <h1 className="register-title-label">
                                    REGISTER
                                </h1>
                            </div>

                            <div className="first-last-container">

                                <Form.Item<VoterType>
                                    name="lastName"
                                    rules={[{ required: true, message: 'Enter your Last Name.' }]}
                                >
                                    <Input
                                        size="large"
                                        maxLength={30}
                                        prefix={
                                            <UserOutlined className="site-form-item-icon" />
                                        }
                                        placeholder="Last Name"
                                    />
                                </Form.Item>

                                <Form.Item<VoterType>
                                    name="firstName"
                                    rules={[{ required: true, message: 'Enter your First Name.' }]}
                                >
                                    <Input
                                        size="large"
                                        maxLength={30}
                                        prefix={
                                            <UserOutlined className="site-form-item-icon" />
                                        }
                                        placeholder="First Name"
                                    />
                                </Form.Item>

                                <Form.Item<VoterType>
                                    name="lastName"
                                >
                                    <Input
                                        size="large"
                                        maxLength={4}
                                        prefix={
                                            <UserOutlined className="site-form-item-icon" />
                                        }
                                        placeholder="Suffix"
                                    />
                                </Form.Item>
                            </div>
                            
                            <Flex gap='middle'>
                                <Form.Item<VoterType>
                                    name="dateOfBirth"
                                    rules={[{ required: true, message: 'Enter your Date of Birth.' }]}
                                > 
                                    <DatePicker
                                        size='large' 
                                        format="MM-DD-YYYY" 
                                        placeholder='Birth Date' 
                                        width={100}
                                        style={{ width: '300px' }}
                                    />
                                </Form.Item>

                                <Form.Item<VoterType>
                                    name="gender"
                                    rules={[{ required: true, message: 'Select your gender.' }]}
                                >
                                    <Flex gap='small' justify='center' align='center'>
                                        <Text style={{ color: 'black', fontSize: '15px' }}>Gender:</Text>

                                        <Radio.Group buttonStyle="solid" size='large'>
                                            <Radio.Button   
                                                value="1" 
                                            > 
                                                Female 
                                            </Radio.Button>
                                            <Radio.Button
                                                value="2" 
                                            > 
                                                Male 
                                            </Radio.Button>
                                            <Radio.Button
                                                value="3" 
                                            > 
                                                Prefer not to say 
                                            </Radio.Button>
                                        </Radio.Group>
                                    </Flex>
                                </Form.Item>
                            </Flex>
                            
                            <Flex gap='middle'>
                                <Form.Item<VoterType>
                                    name="address"
                                    rules={[{ required: true, message: 'Enter your Address.' }]}
                                > 
                                    <Input
                                        size="large"
                                        maxLength={30}
                                        prefix={
                                            <HomeOutlined className="site-form-item-icon" />
                                        }
                                        placeholder="Address"
                                        style={{ 
                                            width: '455px' 
                                        }}
                                    />
                                </Form.Item>
                                <Form.Item<VoterType>
                                    name="zipCode"
                                    rules={[{ required: true, message: 'Enter your Zip Code.' }]}
                                > 
                                    <Input
                                        size="large"
                                        maxLength={30}
                                        prefix={
                                            <NumberOutlined className="site-form-item-icon" />
                                        }
                                        placeholder="Zip Code"
                                        onKeyPress={(event) => {
                                            if (!/[0-9]/.test(event.key)) {
                                                event.preventDefault();
                                            }
                                        }}
                                    />
                                </Form.Item>
                            </Flex>
                            
                            
                            <Flex gap='middle'>
                                <Form.Item<VoterType>
                                        name="religion"
                                        rules={[{ required: true, message: 'Enter your Religion.' }]}
                                    > 
                                    <Input
                                        size="large"
                                        maxLength={30}
                                        prefix={<SolutionOutlined className="site-form-item-icon" />}
                                        placeholder="Religion"
                                        style={{ 
                                            width: '343px' 
                                        }}
                                    />
                                </Form.Item>

                                <Form.Item<VoterType>
                                        name="nationality"
                                        rules={[{ required: true, message: 'Enter your Nationality.' }]}
                                    > 
                                    <Input
                                        size="large"
                                        maxLength={30}
                                        prefix={<SolutionOutlined className="site-form-item-icon" />}
                                        placeholder="Nationality"
                                        style={{ 
                                            width: '343px' 
                                        }}
                                    />
                                </Form.Item>
                            </Flex>
                            
                            <Flex gap='middle' justify='center' align='center'>
                                <Button 
                                    size='large'
                                    type="primary" 
                                    className='next-button-content' 
                                    onClick={next}
                                    loading={loadings}
                                >
                                    Next
                                </Button>
                            </Flex>
                        </Form>
        
                    </React.Fragment>
                )
            case 1: 
                return (
                    <React.Fragment>
                        <Form
                            className="register-form-container"
                            initialValues={{ remember: true }}
                            onFinish={handleRegister}
                            onFinishFailed={onFinishFailed}
                            autoComplete="off"
                        >
                            <Button 
                                className='link-back-content' 
                                onClick={handleBackLogin}    
                            >
                                <ArrowLeftOutlined /> Back
                            </Button> 

                            <div className="image-register-container">
                                <img className="image-register-content" src={Logo} alt="eVotery-logo" />

                                <h1 className="register-title-label">
                                    REGISTER
                                </h1>
                            </div>

                                <Flex gap='middle'>
                                    <Form.Item<VoterType>
                                            name="occupation"
                                            rules={[{ required: true, message: 'Enter your Occupation.' }]}
                                        > 
                                        <Input
                                            size="large"
                                            maxLength={30}
                                            prefix={<SolutionOutlined className="site-form-item-icon" />}
                                            placeholder="Occupation"
                                            style={{ 
                                                width: '343px' 
                                            }}
                                        />
                                    </Form.Item>

                                    <Form.Item<VoterType>
                                        name="phoneNumber"
                                        rules={[{ required: true, message: 'Enter your Phone Number.' }]}
                                    > 

                                        <Input
                                            size="large"
                                            maxLength={15}
                                            prefix={
                                                <PhoneOutlined className="site-form-item-icon" />
                                            }
                                            count={{
                                                show: true,
                                                max: 12,
                                            }}
                                            placeholder="Phone Number"
                                            onKeyPress={(event) => {
                                                if (!/[0-9]/.test(event.key)) {
                                                    event.preventDefault();
                                                }
                                            }}
                                            style={{ 
                                                width: '335px' 
                                            }}
                                            
                                        />
                                    </Form.Item>
                                </Flex>
                                
                                <Form.Item<VoterType>
                                        name="email"
                                        rules={[{ required: true, message: 'Enter your Email.' }]}
                                    > 
                                    <Input
                                        size="large"
                                        maxLength={30}
                                        prefix={<MailOutlined className="site-form-item-icon" />}
                                        type="email"
                                        placeholder="Email"
                                        style={{ 
                                            width: '695px' 
                                        }}
                                        suffix={
                                            <Tooltip title="Email must be valid.">
                                                <InfoCircleOutlined style={{ color: 'rgba(0,0,0,.45)' }} />
                                            </Tooltip>
                                        }
                                    />
                                </Form.Item>
                                
                                <Flex gap='middle'>
                                    <Form.Item<VoterType>
                                        name="password"
                                        rules={[{ required: true, message: 'Enter your Password.' }]}
                                    >
                                        <Input.Password
                                            size="large"
                                            maxLength={8}
                                            prefix={<LockOutlined className="site-form-item-icon" />}
                                            type="password"
                                            placeholder="Password"
                                            iconRender={(visible) => (visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />)}
                                            style={{ width: '340px' }}
                                        />
                                    </Form.Item>
                
                                    <Form.Item<VoterType>
                                        name="confirmPassword"
                                        rules={[
                                            { required: true, message: 'Please confirm your password.' },
                                            ({ getFieldValue }) => ({
                                                validator(_, value) {
                                                    if (!value || getFieldValue('password') === value) {
                                                        return Promise.resolve();
                                                    }
                                                    return Promise.reject(new Error('The two passwords that you entered do not match.'));
                                                },
                                            }),
                                        ]}
                                    >
                                        <Input.Password
                                            size="large"
                                            maxLength={8}
                                            prefix={<LockOutlined className="site-form-item-icon" />}
                                            type="password"
                                            placeholder="Confirm Password"
                                            iconRender={(visible) => (visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />)}
                                            style={{ width: '340px' }}
                                        />
                                    </Form.Item>
                                </Flex>

                                <Form.Item>
                                    <Flex gap='middle' justify='center' align='center'>
                                        
                                        <Button 
                                            size='large'
                                            className='prev-button-content' 
                                            onClick={prev}
                                        >
                                            Previous
                                        </Button>
                                        <Button 
                                            size='large'
                                            type="primary" 
                                            className='next-button-content' 
                                            onClick={next}
                                            loading={loadings}
                                        >
                                            Next
                                        </Button>
                                    </Flex>
                                </Form.Item>
                        </Form>
                    </React.Fragment>
                )
            case 2:
                return (
                    <React.Fragment>
                        <Form
                            className="register-form-container"
                            initialValues={{ remember: true }}
                            onFinish={handleRegister}
                            onFinishFailed={onFinishFailed}
                            autoComplete="off"
                        >
                            <Button 
                                className='link-back-content' 
                                onClick={handleBackLogin}    
                            >
                                <ArrowLeftOutlined /> Back
                            </Button>   

                            <div className="image-register-container">
                                <img className="image-register-content" src={Logo} alt="eVotery-logo" />

                                <h1 className="register-title-label">
                                    REGISTER
                                </h1>
                            </div>

                            <Form.Item<VoterType>
                                name="personalDocumentId"
                                rules={[{ required: true, message: 'Please select a documents' }]}
                            >
                                <Select 
                                    placeholder='Select a Documents'
                                    size='large'
                                >
                                    {
                                        personalDocuments.map(
                                            documents => (
                                                <Select.Option
                                                    key={documents.id}
                                                    value={documents.id}
                                                >
                                                    {documents.document}
                                                </Select.Option>
                                            )
                                        )
                                    }
                                </Select>
                            </Form.Item>

                            <Form.Item<VoterType>
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
                                <Dragger {...props} listType='picture' style={{  width: '100%', height: '200px' }}>
                                    <p className="ant-upload-drag-icon">
                                        <InboxOutlined />
                                    </p>
                                    <p className="ant-upload-text">Click or drag a valid ID file to this area to upload</p>
                                    <p className="ant-upload-hint">
                                        Please upload a valid identification document. Supported file types include JPG, PNG, or PDF.
                                    </p>
                                </Dragger>
                            </Form.Item>

                            <Form.Item<VoterType>
                                    name="idNUmber"
                                    rules={[{ required: true, message: 'Enter your Id Number.' }]}
                                > 
                                <Input
                                    size="large"
                                    maxLength={30}
                                    prefix={<NumberOutlined className="site-form-item-icon" />}
                                    type="email"
                                    placeholder="Id Number"
                                    style={{ 
                                        width: '695px' 
                                    }}
                                    suffix={
                                        <Tooltip title="Email must be valid id number.">
                                            <InfoCircleOutlined style={{ color: 'rgba(0,0,0,.45)' }} />
                                        </Tooltip>
                                    }
                                />
                            </Form.Item>
                            
                            <div className="terms-agreement-container">
                                <Form.Item
                                    name='hasAgreedToTerms'
                                >
                                    <Checkbox value='True'>
                                        I have read and agree to the <a href="/terms-and-conditions" style={{ color: '#1890ff' }}>
                                            Terms and Conditions
                                        </a> and <a href="/privacy-policy" style={{ color: '#1890ff' }}>
                                            Privacy Policy
                                        </a>
                                    </Checkbox>
                                </Form.Item>
                            </div>
                            <Form.Item>
                                <Flex gap='middle' justify='center' align='center'>
                                        <Button 
                                            size='large'
                                            className='prev-button-content' 
                                            onClick={prev}
                                        >
                                            Previous
                                        </Button>
                                        <Button 
                                            size='large'
                                            type="primary" 
                                            className='next-button-content' 
                                            onClick={next}
                                            loading={loadings}
                                        >
                                            Register
                                        </Button>
                                </Flex>
                            </Form.Item>
                        </Form>
                    </React.Fragment>
                )
        }
    };

    const steps = [
        {
            title: 'Step 1',
            content: renderFormContent(0)
        },
        {
            title: 'Step 2',
            content: renderFormContent(1)
        },
        {
            title: 'Step 3',
            content: renderFormContent(2)
        }
    ];
    

    const items = steps.map((item) => ({ key: item.title, title: item.title }));

    return (
        <div className="register-main-container">
            <div className="step-line-container">
                <Steps current={current} items={items}>
                    <div style={contentStyle}>{steps[current].content}</div>
                </Steps>
            </div>
            
            <React.Fragment>
                {steps[current].content}
            </React.Fragment>
                
        </div>
    );
}


export default Register