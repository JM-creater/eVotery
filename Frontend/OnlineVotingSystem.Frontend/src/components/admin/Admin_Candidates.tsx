import { 
    EditOutlined, 
    EllipsisOutlined, 
    PlusOutlined, 
    SettingOutlined, 
    UploadOutlined 
} from '@ant-design/icons'
import { 
    Avatar, 
    Button, 
    Card, 
    Col, 
    Drawer, 
    Dropdown, 
    FloatButton, 
    Form, 
    Input, 
    Menu,  
    Popconfirm,  
    Radio, 
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
const GETALL_PARTYAFFILIATION_URL = 'https://localhost:7196/PartyAffiliation/getall-party';
const UPDATE_CANDIDATE_URL = 'https://localhost:7196/Candidate/update-candidate/';
const DELETE_CANDIDATE_URL = 'https://localhost:7196/Candidate/delete-candidate/';
const SEARCH_CANDIDATE_URL = 'https://localhost:7196/Search/search-candidate?searchQuery=';
const ACTIVATE_CANDIDATE_URL = 'https://localhost:7196/Candidate/activate-candidate/';
const DEACTIVATE_CANDIDATE_URL = 'https://localhost:7196/Candidate/deactivate-candidate/';

const { Meta } = Card;

type CandidateType = {
    id?: string;
    firstName?: string;
    lastName?: string;
    image?: FileType[];
    ballotId?: string;
    positionId?: string;
    partyAffiliationId?: string;
    gender?: string;
    biography?: string;
    status?: CandidateStataus[];
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
    ballotName?: string;
}

type PartyAffiliationType = {
    id?: string;
    partyName?: string;
}

enum CandidateStataus {
    Active = 1,
    Inactive = 2,
    Disqualified = 3
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

    const [open, setOpen] = useState<boolean>(false);
    const [editOpen, setEditOpen] = useState<boolean>(false); 
    const [candidates, setCandidates] = useState<CandidateType[]>([]);
    const [filteredCandidates, setFilteredCandidates] = useState<CandidateType[]>([]);
    const [positions, setPositions] = useState<PositionType[]>([]);
    const [ballots, setBallots] = useState<BallotType[]>([]);
    const [affiliate, setAffiliate] = useState<PartyAffiliationType[]>([]);
    const [form] = Form.useForm();
    const [selectedCandidate, setSelectedCandidate] = useState<CandidateType | null>(null);

    const showDrawer = () => {
        setOpen(true);
    };

    const closeDrawer = () => {
        setOpen(false);
        form.resetFields();
    };

    const showEditDrawer = (values: CandidateType) => {
        setSelectedCandidate(values);
        setEditOpen(true);
    };

    const closeEditDrawer = () => {
        setEditOpen(false);
    };

    const handleCancel = (e?: React.MouseEvent<HTMLElement, MouseEvent>) => {
        console.log(e);
    };

    useEffect(() => {
        const fetchCandidates = async () => {
            try {
                const response = await axios.get(GETALL_CANDIDATE_URL);
                setCandidates(response.data);
                setFilteredCandidates(response.data);
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

    useEffect(() => {
        const fetchPartyAffiliation = async () => {
            try {
                const response = await axios.get(GETALL_PARTYAFFILIATION_URL);
                setAffiliate(response.data);
            } catch (error) {
                console.error(error);
            }
        }

        fetchPartyAffiliation();
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
                const newCandidate = response.data.result;
                setCandidates(prevCandidate => [...prevCandidate, newCandidate ]);
                setFilteredCandidates(prevCandidate => [...prevCandidate, newCandidate])
                setOpen(false);
                form.resetFields();
            } else if (response.data.responseCode === 400) {
                toast.error('Create a candidate failed.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }

        } catch (error) {
            console.error(error);
        }
    };

    const handleUpdateCandidate = async (id: string, values: CandidateType) => {
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

            const response = await axios.put(`${UPDATE_CANDIDATE_URL}${id}`, formData, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            });

            if (response.data.responseCode === 200) {
                const updatedResponse = await axios.get(GETALL_CANDIDATE_URL);
                setCandidates(updatedResponse.data)
                setFilteredCandidates(updatedResponse.data);

                toast.success('Successfully Updated a Candidate.');
                setEditOpen(false);
            } else if (response.data.responseCode === 400) {
                toast.error('Create a candidate failed.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }

        } catch (error) {
            console.error(error);
        }
    };

    const handleDeleteCandidate = async (id: string) => {
        try {
            const response = await axios.delete(`${DELETE_CANDIDATE_URL}${id}`);

            if (response.data.responseCode === 200) {
                toast.success('Successfully Deleted a Candidate');
                setCandidates(prevCandidates => prevCandidates.filter(candidate => candidate.id == id));
            } else if (response.data.responseCode === 400 || response.data.responseCode === 500) {
                toast.error('Failed to delete the candaite.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }
        } catch (error) {
            console.error(error);
        }
    };

    const handleSearchCandidate = async (searchQuery: string | null | undefined) => {
        if (searchQuery === null || searchQuery === undefined) {
            return setFilteredCandidates([]);
        }

        const trimmedQuery = searchQuery.trim();

        if (!trimmedQuery) {
            setFilteredCandidates(candidates);
            return;
        }

        const controller = new AbortController();
        const { signal } = controller;

        try {
            const response = await axios.get(`${SEARCH_CANDIDATE_URL}${encodeURIComponent(searchQuery)}`, { signal });
            if (response.data && !Array.isArray(response.data)) {
                setFilteredCandidates([response.data]);
            } else {
                setFilteredCandidates([]);
                toast.info('No candidate found matching the search criteria.');
            }
        } catch (error) {
            console.error(error);
        }

        return () => controller.abort();
    };

    const handleActivateCandidate = async (id: string) => {
        try {
            const response = await axios.put(`${ACTIVATE_CANDIDATE_URL}${id}`);
            return response.data;
        } catch (error) {
            console.error(error);
        }
    };

    const handleDeactivateCandidate = async (id: string) => {
        try {
            const response = await axios.put(`${DEACTIVATE_CANDIDATE_URL}${id}`);
            return response.data;
        } catch (error) {
            console.error(error);
        }
    };

    const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === "Enter") {
            const value = (event.currentTarget as HTMLInputElement).value;
            handleSearchCandidate(value);
        } 
    };
    

    const getPositionName = (positionId: string) => {
        const position = positions.find(p => p.id == positionId)  
        return position ? position.name : 'No Position Found';
    };

    const items = [
        {
            key: "1",
            label: (
                <a onClick={() => handleActivateCandidate(selectedCandidate?.id as string)}>
                    Activate
                </a>
            ),
        },
        {
            key: "2",
            label: (
                <a onClick={() => handleDeactivateCandidate(selectedCandidate?.id as string)}>
                    Deactivate
                </a>
            ),
        },
    ];

    const menu: React.ReactElement = (
        <Menu>
            {items.map(item => (
                <Menu.Item key={item.key}>
                    {item.label}
                </Menu.Item>
            ))}
        </Menu>
    );

    return (
        <React.Fragment>

            <React.Fragment>
                <Row justify="space-between" align="middle">
                    <Col>
                        <FloatButton 
                            icon={<PlusOutlined/>} 
                            type='primary' 
                            style={{ 
                                width: '60px', 
                                height: '60px' 
                            }} 
                            onClick={showDrawer}
                        />
                    </Col>
                    <Col>
                        <Input.Search 
                            onSearch={(value: string) => handleSearchCandidate(value)}
                            onKeyDown={handleKeyDown}
                            placeholder="Search..." 
                            style={{ width: 300 }} 
                        />
                    </Col>
                </Row>
            </React.Fragment>

            <div className="card-main-candidate">
                
                <React.Fragment>
                    {
                        filteredCandidates.map(
                            candidate => (
                                <Card
                                    key={candidate.id}
                                    hoverable
                                    style={{ width: 300 }}
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
                                        <Popconfirm
                                            title="Delete Candidate"
                                            description="Are you sure you want to delete this candidates?"
                                            onConfirm={() => handleDeleteCandidate(candidate.id as string)}
                                            onCancel={handleCancel}
                                            okText="Confirm"
                                            cancelText="Cancel"
                                        >
                                            <SettingOutlined key="setting" />,
                                        </Popconfirm>,
                                        <EditOutlined 
                                            key="edit" 
                                            onClick={() => showEditDrawer(candidate as CandidateType)} 
                                        />,
                                        <Dropdown overlay={menu} placement="bottom" arrow>  
                                            <EllipsisOutlined key="ellipsis" />
                                        </Dropdown>
                                    ]}
                                >
                                    <Meta
                                        avatar={
                                            <Avatar 
                                                src={
                                                    candidate.gender === "1"
                                                    ? "https://api.dicebear.com/7.x/miniavs/svg?seed=9" 
                                                    : "https://api.dicebear.com/7.x/miniavs/svg?seed=8"} 
                                                />
                                        }
                                        title={`${candidate.firstName} ${candidate.lastName}`}
                                        description={getPositionName(candidate.positionId as string)}
                                    />
                                </Card>
                            )
                        )
                    }
                </React.Fragment>

                {/* Add Drawer */}

                <Drawer 
                    maskClosable={false}
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
                        form={form}
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
                                                        {ballot.ballotName}
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
                                    name="partyAffiliationId"
                                    label="Select Party Affiliate"
                                    rules={[{ required: true, message: 'Please choose party' }]}
                                >
                                    <Select 
                                        placeholder="Please choose party"
                                    >
                                        {
                                            affiliate.map(
                                                partyAffiliate => (
                                                    <Select.Option 
                                                        key={partyAffiliate.id}
                                                        value={partyAffiliate.id}
                                                    >
                                                        {partyAffiliate.partyName}
                                                    </Select.Option>
                                                )
                                            )
                                        }
                                    </Select>
                                </Form.Item>
                            </Col>
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
                        <Row gutter={16}>
                            <Col span={12}>
                                <Form.Item<CandidateType>
                                    name="biography"
                                    label="Biography"
                                    rules={[
                                        {
                                            required: true,
                                            message: 'please enter biography',
                                        },
                                    ]}
                                >
                                    <Input.TextArea rows={4} placeholder="please enter biography" />
                                </Form.Item>
                            </Col>
                            <Col span={12}>
                                <Form.Item<CandidateType>
                                    name="gender"
                                    label="Gender"
                                    rules={[{ required: true, message: 'Please select gender' }]}
                                >
                                    <Radio.Group>
                                        <Radio value="1"> Female </Radio>
                                        <Radio value="2"> Male </Radio>
                                    </Radio.Group>
                                </Form.Item>
                            </Col>
                        </Row>
                    </Form>
                </Drawer>

                {/* Edit Drawer */}

                <Drawer 
                    maskClosable={false}
                    title="Create a new candidate" 
                    onClose={closeEditDrawer} 
                    open={editOpen}
                    width={720}
                    styles={{
                        body: {
                            paddingBottom: 80,
                        },
                    }}
                    footer={
                        <Space style={{ float: 'right', paddingBottom: 8 }}>
                            <Button onClick={closeEditDrawer} type="primary" danger>
                                Cancel
                            </Button>
                            <Button form="create-candidate-form" key="submit" htmlType="submit" type="primary">
                                Update
                            </Button>
                        </Space>
                    }
                >
                    {
                        selectedCandidate && (
                            <Form 
                                id="create-candidate-form" 
                                key={selectedCandidate.id}
                                onFinish={(values: CandidateType) => handleUpdateCandidate(selectedCandidate.id as string, values)}
                                onFinishFailed={onFinishFailed}
                                layout="vertical"
                            >

                                <Row gutter={16}>
                                    <Col span={12}>
                                        <Form.Item<CandidateType>
                                            name="firstName"
                                            label="First Name"
                                            initialValue={selectedCandidate.firstName}
                                            rules={[{ required: true, message: 'Please enter first name' }]}
                                        >
                                            <Input placeholder="Please enter first name" />
                                        </Form.Item>
                                    </Col>
                                    <Col span={12}>
                                        <Form.Item<CandidateType>
                                            name="lastName"
                                            label="Last Name"
                                            initialValue={selectedCandidate.lastName}
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
                                            initialValue={selectedCandidate.ballotId}
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
                                                                {ballot.ballotName}
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
                                            initialValue={selectedCandidate.positionId}
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
                                            name="partyAffiliationId"
                                            label="Select Party Affiliate"
                                            initialValue={selectedCandidate.partyAffiliationId}
                                            rules={[{ required: true, message: 'Please choose party' }]}
                                        >
                                            <Select 
                                                placeholder="Please choose party"
                                            >
                                                {
                                                    affiliate.map(
                                                        partyAffiliate => (
                                                            <Select.Option 
                                                                key={partyAffiliate.id}
                                                                value={partyAffiliate.id}
                                                            >
                                                                {partyAffiliate.partyName}
                                                            </Select.Option>
                                                        )
                                                    )
                                                }
                                            </Select>
                                        </Form.Item>
                                    </Col>
                                    <Col span={12}>
                                        <Form.Item<CandidateType>
                                            name="image"
                                            label="Image"
                                            initialValue={selectedCandidate.image ? [{ uid: '-1', name: selectedCandidate.image, status: 'done', url: `https://localhost:7196/${selectedCandidate.image}` }] : []}
                                            rules={[{ 
                                                required: true, message: 'Please upload an image.' 
                                            }]}
                                            valuePropName="fileList"
                                            getValueFromEvent={(e) => {
                                                if (Array.isArray(e)) {
                                                    return e;
                                                } else if (e && e.fileList && Array.isArray(e.fileList)) {
                                                    return e.fileList;
                                                } else {
                                                    return [];
                                                }
                                            }}
                                        >
                                            <Upload {...props} listType="picture">
                                                <Button icon={<UploadOutlined/>}>Upload Image</Button>
                                            </Upload>
                                        </Form.Item>
                                    </Col>
                                </Row>
                                <Row gutter={16}>
                                    <Col span={12}>
                                        <Form.Item<CandidateType>
                                            name="biography"
                                            label="Biography"
                                            initialValue={selectedCandidate.biography}
                                            rules={[
                                                {
                                                    required: true,
                                                    message: 'please enter biography',
                                                },
                                            ]}
                                        >
                                            <Input.TextArea rows={4} placeholder="please enter biography" />
                                        </Form.Item>
                                    </Col>
                                    <Col span={12}>
                                        <Form.Item<CandidateType>
                                            name="gender"
                                            label="Gender"
                                            initialValue={selectedCandidate.gender}
                                            rules={[{ required: true, message: 'Please select gender' }]}
                                        >
                                            <Radio.Group>
                                                <Radio value="1"> Female </Radio>
                                                <Radio value="2"> Male </Radio>
                                            </Radio.Group>
                                        </Form.Item>
                                    </Col>
                                </Row>
                            </Form>
                        )
                    }
                    
                </Drawer>

            </div>

        </React.Fragment>
    )
}

export default Admin_Candidates