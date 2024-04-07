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

const ONESTEP_REGISTER_URL = 'https://localhost:7196/User/register-first-step';
const TWOSTEP_REGISTER_URL = 'https://localhost:7196/User/register-second-step/';
const THREESTEP_REGISTER_URL = 'https://localhost:7196/User/register-third-step/';
const THREESUBSTEP_REGISTER_URL = 'https://localhost:7196/User/register-subthird-step/';
const GETALL_DOCUMENTS_URL = 'https://localhost:7196/PersonalDocument/getall-documents';

type StepOneType = {
    firstName?: string;
    lastName?: string;
    suffixName?: string;
    dateOfBirth?: string;
    gender?: string;
    address?: string;
    nationality?: string;
    religion?: string;
    zipCode?: string;
}

type StepTwoType = {
    id?: string;
    occupation?: string;
    phoneNumber?: string;
    email?: string;
    password?: string;
    confirmPassword?: string;
}

type StepThreeType = {
    personalDocumentId?: string;
    hasAgreedToTerms?: boolean;
}

type StepSubThreeType = {
    pidNumber?: string;
    pImage?: FileType[];
}

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

type CombineTwoTypes = StepThreeType & StepSubThreeType;

const props: UploadProps = {
    name: 'pImage',
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
    const [form] = Form.useForm();

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

    // * First Step Register Handle
    const handleStepOneRegister = async (values: StepOneType) => {
        setLoadings(true);

        const oneStepRequest = {
            FirstName: values.firstName,
            LastName: values.lastName,
            SuffixName: values.suffixName,
            DateOfBirth: values.dateOfBirth,
            Gender: values.gender,
            Address: values.address,
            Nationality: values.nationality,
            Religion: values.religion,
            ZipCode: values.zipCode
        }

        try {
            const response = await axios.post(ONESTEP_REGISTER_URL, oneStepRequest, {
                headers: {
                    'Content-Type': 'application/json',
                },
            })

            if (response.data.responseCode === 200) {
                if (current < steps.length - 1) {
                    const getUserId = response.data.result.id;

                    localStorage.setItem('userId', getUserId);

                    setCurrent((prevCurrent) => prevCurrent + 1);
                    form.resetFields();
                }
            } else if (response.data.responseCode === 400) {
                toast.error('Register failed. Please check your information.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }
        } catch (error) {
            console.error(error);
        } finally {
            setLoadings(false);
        }
    };

    // * Second Step Register Handle
    const handleStepTwoRegister = async (values: StepTwoType) => {
        setLoadings(true);

        const twoStepRequest = {
            Occupation: values.occupation,
            PhoneNumber: values.phoneNumber,
            Email: values.email,
            Password: values.password
        }

        const id = localStorage.getItem('userId');

        try {
            const response = await axios.put(`${TWOSTEP_REGISTER_URL}${id}`, twoStepRequest, {
                headers: {
                    'Content-Type': 'application/json',
                },
            })

            if (response.data.responseCode === 200) {
                if (current < steps.length - 1) {
                    setCurrent((prevCurrent) => prevCurrent + 1);
                    form.resetFields();
                }
            } else if (response.data.responseCode === 400) {
                toast.error('Register failed. Please check your information.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }
        } catch (error) {
            console.error(error);
        } finally {
            setLoadings(false);
        }
    };

    // * Third Step Register Handle
    const handleStepThreeRegister = async (values: StepThreeType) => {
        setLoadings(true);

        const threeStepRequest = {
            PersonalDocumentId: values.personalDocumentId,
            hasAgreedToTerms: values.hasAgreedToTerms
        }

        const id = localStorage.getItem('userId');

        try {
            const response = await axios.put(`${THREESTEP_REGISTER_URL}${id}`, threeStepRequest, {
                headers: {
                    'Content-Type': 'application/json',
                },
            })

            if (response.data.responseCode === 200) {
                if (current < steps.length - 1) {
                    setCurrent((prevCurrent) => prevCurrent + 1);
                    form.resetFields();
                }
            } else if (response.data.responseCode === 400) {
                toast.error('Register failed. Please check your information.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }
        } catch (error) {
            console.error(error);
        } finally {
            setLoadings(false);
        }
    };

    // * Sub Third Step Register Handle
    const handleStepSubThreeRegister = async (values: StepSubThreeType) => {
        setLoadings(true);

        const id = localStorage.getItem('userId');

        try {
            const formData = new FormData();

            Object.entries(values).forEach(([key, value]) => {
                if (key !== 'pImage' && value !== undefined) {
                    formData.append(key, value.toString());
                }
            })

            if (values.pImage && values.pImage.length > 0) {
                const file = values.pImage[0].originFileObj;
                if (file) {
                    formData.append('pImage', file);
                }
            }

            const response = await axios.put(`${THREESUBSTEP_REGISTER_URL}${id}`, formData, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            })

            if (response.data.responseCode === 200) {
                await delay(1000);
                localStorage.removeItem('userId');
                navigate('/');
            } else if (response.data.responseCode === 400) {
                toast.error('Register failed. Please check your information.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }
        } catch (error) {
            console.error(error);
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
                            onFinish={(values: StepOneType) => handleStepOneRegister(values)}
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

                                <Form.Item<StepOneType>
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

                                <Form.Item<StepOneType>
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

                                <Form.Item<StepOneType>
                                    name="suffixName"
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
                                <Form.Item<StepOneType>
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

                                <Form.Item<StepOneType>
                                    name="gender"
                                    rules={[{ required: true, message: 'Select your gender.' }]}
                                >
                                    <Flex gap='small' justify='center' align='center'>
                                        <Text style={{ color: 'black', fontSize: '15px' }}>Gender:</Text>

                                        <Radio.Group buttonStyle="solid" size='large'>
                                            <Radio.Button   
                                                value="Female" 
                                            > 
                                                Female 
                                            </Radio.Button>
                                            <Radio.Button
                                                value="Male" 
                                            > 
                                                Male 
                                            </Radio.Button>
                                            <Radio.Button
                                                value="Prefer not to say" 
                                            > 
                                                Prefer not to say 
                                            </Radio.Button>
                                        </Radio.Group>
                                    </Flex>
                                </Form.Item>
                            </Flex>
                            
                            <Flex gap='middle'>
                                <Form.Item<StepOneType>
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
                                <Form.Item<StepOneType>
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
                                <Form.Item<StepOneType>
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

                                <Form.Item<StepOneType>
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
                            
                            <Form.Item>
                                <Flex gap='middle' justify='center' align='center'>
                                    <Button 
                                        size='large'
                                        type="primary" 
                                        className='next-button-content' 
                                        htmlType="submit" 
                                        loading={loadings}
                                    >   
                                        Next
                                    </Button>
                                </Flex>
                            </Form.Item>
                            
                        </Form>
        
                    </React.Fragment>
                )
            case 1: 
                return (
                    <React.Fragment>
                        <Form
                            className="register-form-container"
                            initialValues={{ remember: true }}
                            onFinish={(values: StepTwoType) => handleStepTwoRegister(values as StepTwoType)}
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
                                    <Form.Item<StepTwoType>
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

                                    <Form.Item<StepTwoType>
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
                                
                                <Form.Item<StepTwoType>
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
                                    <Form.Item<StepTwoType>
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
                
                                    <Form.Item<StepTwoType>
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
                                            htmlType="submit" 
                                            className='next-button-content' 
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
                            onFinish={(values: CombineTwoTypes) => {
                                const { personalDocumentId, hasAgreedToTerms, pidNumber, pImage } = values;

                                // * Process StepThreeType values
                                const stepThreeValues: StepThreeType = { personalDocumentId, hasAgreedToTerms };
                                handleStepThreeRegister(stepThreeValues);
                                
                                // * Process StepSubThreeType values
                                const stepSubThreeValues: StepSubThreeType = { pidNumber, pImage };
                                handleStepSubThreeRegister(stepSubThreeValues);
                            }}
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

                            <Form.Item<StepThreeType>
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

                            <Form.Item<StepSubThreeType>
                                name="pImage"
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

                            <Form.Item<StepSubThreeType>
                                    name="pidNumber"
                                    rules={[{ required: true, message: 'Enter your Id Number.' }]}
                                > 
                                <Input
                                    size="large"
                                    maxLength={30}
                                    prefix={<NumberOutlined className="site-form-item-icon" />}
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
                                <Form.Item<StepThreeType>
                                    name='hasAgreedToTerms'
                                    // rules={[{ required: true, message: 'Please check the terms and agreements.' }]}
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
                                            htmlType="submit" 
                                            className='next-button-content' 
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