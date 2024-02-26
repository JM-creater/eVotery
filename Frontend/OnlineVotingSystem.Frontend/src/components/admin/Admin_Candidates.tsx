import { EditOutlined, EllipsisOutlined, PlusOutlined, SettingOutlined, UploadOutlined } from '@ant-design/icons'
import { 
    Avatar, 
    Button, 
    Card, 
    Col, 
    Drawer, 
    Form, 
    Input, 
    Row, 
    Select, 
    Space, 
    Upload, 
    UploadProps, 
    message
} from 'antd'
import React, { useEffect, useState } from 'react'
import '../admin/Admin_Candidates.css'
import axios from 'axios';
import { toast } from 'react-toastify';

const CREATE_CANDIDATE_URL = 'https://localhost:7196/Candidate/create';
const GETALL_CANDIDATE_URL = 'https://localhost:7196/Candidate/get-all';
const GETALL_POSITION_URL = 'https://localhost:7196/Position/get-all';
const GETALL_BALLOTS_URL = 'https://localhost:7196/Ballot/getall-ballots';

const { Meta } = Card;

type CandidateType = {
    id?: string;
    firstName?: string;
    lastName?: string;
    image?: FileType[];
    ballotId?: string;
    positionId?: string;
}

type FileType = {
    uid: string;
    name: string;
    status: string;
    originFileObj: File;
}

type PositionType = {
    id?: string;
    name?: string;
}

type BallotType = {
    id?: string;
    name?: string;
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

const Admin_Candidates:React.FC = () => {

    const [candidates, setCandidates] = useState<CandidateType[]>([]);
    const [open, setOpen] = useState<boolean>(false);
    const [positions, setPositions] = useState<PositionType[]>([]);
    const [ballots, setBallots] = useState<BallotType[]>([]);

    const showDrawer = () => {
        setOpen(true);
    };

    const closeDrawer = () => {
        setOpen(false);
    };

    useEffect(() => {
        const fetchCandidates = async () => {
            try {
                const response = await axios.get(GETALL_CANDIDATE_URL);
                setCandidates(response.data);
            } catch (error) {
                console.error(error);
            }
        }

        fetchCandidates();
    }, []);

    useEffect(() => {
        const fetchPositions = async () => {
            try {
                const response = await axios.get(GETALL_POSITION_URL);
                setPositions(response.data);
            } catch (error) {
                console.error(error);
            }
        }

        fetchPositions();
    }, []);

    useEffect(() => {
        const fetchBallots = async () => {
            try {
                const response = await axios.get(GETALL_BALLOTS_URL);
                setBallots(response.data);
            } catch (error) {
                console.error(error);
            }
        }

        fetchBallots();
    }, []);

    const handleCreateCandidate = async (values: CandidateType) => {

        try {
            const formData = new FormData();

            Object.entries(values).forEach(([key, value]) => {
                if (key !== 'image' && value !== undefined) {
                    formData.append(key, value.toString());
                }
            });

            if (values.image && values.image.length > 0) {
                const file = values.image[0].originFileObj;

                if (file) {
                    formData.append('image', file);
                }
            }

            const response = await axios.post(CREATE_CANDIDATE_URL, formData, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            })

            if (response.data.responseCode === 200) {
                toast.success('Successfully Created a Candidate.');
                setOpen(false);
                setCandidates(prevCandidates => [...prevCandidates, response.data.newCandidates])
            } else if (response.data.responseCode === 400) {
                toast.error('Create a candidate failed.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }

        } catch (error) {
            console.error(error);
        }
    };

    const getPositionName = (positionId: string) => {
        const position = positions.find(p => p.id == positionId)  
        return position ? position.name : 'No Position Found';
    };

    return (
        <React.Fragment>

            <div className="title-candidate-container">
                <h3 className='title-candidate-content'>Candidates</h3>
            </div>

            <div className="card-main-candidate">
                
                {/* <div className="add-candidate-container" onClick={showDrawer}>
                    <div className="plus-icon-container">
                        <div className="plus-icon-content">
                            <PlusOutlined style={{ fontSize: '800%', color: 'white'}} />
                        </div>
                        <div className="div-title-container">
                            <h3>Add Candidate</h3>
                        </div>
                    </div>
                </div> */}

                {
                    candidates.map(
                        candidate => (
                            <Card
                                key={candidate.id}
                                hoverable
                                style={{ width: 240 }}
                                cover={
                                    <div 
                                        style={{ 
                                            height: 160,
                                            overflow: 'hidden'
                                        }}
                                    >
                                        <img
                                            alt="candidate-image"
                                            src={`https://localhost:7196/${candidate.image}`}
                                            style={{ 
                                                width: '100%',
                                                height: '100%',
                                                objectFit: 'cover'
                                            }}
                                        />
                                    </div>
                                }
                                actions={[
                                    <SettingOutlined key="setting" />,
                                    <EditOutlined key="edit" />,
                                    <EllipsisOutlined key="ellipsis" />,
                                ]}
                            >
                                <Meta
                                    avatar={<Avatar src="https://api.dicebear.com/7.x/miniavs/svg?seed=8" />}
                                    title={`${candidate.firstName} ${candidate.lastName}`}
                                    description={getPositionName(candidate.positionId as string)}
                                />
                            </Card>
                        )
                    )
                }

                <Drawer 
                    title="Create a new candidate" 
                    onClose={closeDrawer} 
                    open={open}
                    width={720}
                    styles={{
                        body: {
                            paddingBottom: 80,
                        },
                    }}
                    footer={
                        <Space style={{ float: 'right', paddingBottom: 8 }}>
                            <Button onClick={closeDrawer} type="primary" danger>
                                Cancel
                            </Button>
                            <Button form="create-candidate-form" key="submit" htmlType="submit" type="primary">
                                Create
                            </Button>
                        </Space>
                    }
                >
                    <Form 
                        id="create-candidate-form" 
                        onFinish={handleCreateCandidate}
                        onFinishFailed={onFinishFailed}
                        layout="vertical"
                    >

                        <Row gutter={16}>
                            <Col span={12}>
                                <Form.Item<CandidateType>
                                    name="firstName"
                                    label="First Name"
                                    rules={[{ required: true, message: 'Please enter first name' }]}
                                >
                                    <Input placeholder="Please enter first name" />
                                </Form.Item>
                            </Col>
                            <Col span={12}>
                                <Form.Item<CandidateType>
                                    name="lastName"
                                    label="Last Name"
                                    rules={[{ required: true, message: 'Please enter last name' }]}
                                >
                                    <Input placeholder="Please enter last name" />
                                </Form.Item>
                            </Col>
                        </Row>

                        <Row gutter={16}>
                            <Col span={12}>
                            <Form.Item<CandidateType>
                                name="ballotId"
                                label="Ballot"
                                rules={[{ required: true, message: 'Please select a ballot' }]}
                            >
                                <Select 
                                    placeholder="Please select a ballot"
                                >
                                    {
                                        ballots.map(
                                            ballot => (
                                                <Select.Option 
                                                    key={ballot.id}
                                                    value={ballot.id}
                                                >
                                                    {ballot.name}
                                                </Select.Option>
                                            )
                                        )
                                    }
                                </Select>
                            </Form.Item>
                            </Col>
                            <Col span={12}>
                            <Form.Item<CandidateType>
                                name="positionId"
                                label="Select Position"
                                rules={[{ required: true, message: 'Please choose position' }]}
                            >
                                <Select 
                                    placeholder="Please choose position"
                                >
                                    {
                                        positions.map(
                                            position => (
                                                <Select.Option 
                                                    key={position.id}
                                                    value={position.id}
                                                >
                                                    {position.name}
                                                </Select.Option>
                                            )
                                        )
                                    }
                                </Select>
                            </Form.Item>
                            </Col>
                        </Row>

                        <Row gutter={16}>
                            <Col span={12}>
                                <Form.Item<CandidateType>
                                    name="image"
                                    label="Image"
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
                            </Col>
                        </Row>
                    </Form>
                </Drawer>
            </div>

        </React.Fragment>
    )
}

export default Admin_Candidates