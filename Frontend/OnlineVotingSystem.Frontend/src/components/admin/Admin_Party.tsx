import React, { useEffect, useState } from 'react'
import '../admin/Admin_Party.css'
import { LoadingOutlined, UploadOutlined } from '@ant-design/icons';
import { 
    Spin, 
    Table, 
    Image, 
    Space, 
    Button, 
    Popconfirm, 
    Modal, 
    Form, 
    Row, 
    Col, 
    Input, 
    Upload, 
    UploadProps, 
    message 
} from 'antd';
import axios from 'axios';
import Search from 'antd/es/input/Search';
import { toast } from 'react-toastify';

const GET_ALL_PARTY = 'https://localhost:7196/PartyAffiliation/getall-party';
const CREATE_PARTY_URL = 'https://localhost:7196/PartyAffiliation/create-party';
const UPDATE_PARTY_URL = 'https://localhost:7196/PartyAffiliation/update-party/';
const DELETE_PARTY_URL = 'https://localhost:7196/PartyAffiliation/delete-party/';
const SEARCH_PARTY_URL = 'https://localhost:7196/Search/search-party?searchQuery=';
const GET_PARTYMEMBERS_URL = 'https://localhost:7196/PartyAffiliation/get-count-member/'; 

type PartyAffiliationType = {
    id?: string;
    partyName?: string;
    logoImage?: FileType[];
}

type FileType = {
    uid: string;
    name: string;
    status: string;
    originFileObj: File;
}

const props: UploadProps = {
    name: 'logoImage',
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

const Admin_Party: React.FC = () => {

    const[isLoading, setIsLoading] = useState<boolean>(false);
    const[party, setParty] = useState<PartyAffiliationType[]>([]);
    const[filteredParty, setFilteredParty] = useState<PartyAffiliationType[]>([]);
    const[isModalOpen, setIsModalOpen] = useState<boolean>(false);
    const[isEditModalOpen, setIsEditModalOpen] = useState<boolean>(false);
    const[selectedParty, setSelectedParty] = useState<PartyAffiliationType | null>(null);
    const[countMembers, setCountMembers] = useState<number>(0);

    const antIcon = <LoadingOutlined style={{ fontSize: 50 }} spin />

    useEffect(() => {
        const fetchParty = async () => {
            try {
                const response = await axios.get(GET_ALL_PARTY);
                setIsLoading(true);
                setParty(
                    response.data.map(
                        (
                            row: {
                                partyName: PartyAffiliationType;
                                logoImage: PartyAffiliationType;
                            }
                        ) => (
                            {
                                partyName: row.partyName,
                                logoImage: row.logoImage,
                            }
                        )
                    )
                )
                setFilteredParty(response.data);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
            }
        }

        fetchParty();
    }, []);


    const handleCancel = (e?: React.MouseEvent<HTMLElement, MouseEvent>) => {
        console.log(e);
    };

    const showModal = () => {
        setIsModalOpen(true);
    };

    const exitModal = () => {
        setIsModalOpen(false);
    };

    const showEditModal = (record: PartyAffiliationType) => {
        setSelectedParty(record);
        setIsEditModalOpen(true);
    }

    const exitEditModal = () => {
        setIsEditModalOpen(false);
    }

    useEffect(() => {
        const fetchPartyMembers = async () => {
            try {
                const response = await axios.get(`${GET_PARTYMEMBERS_URL}${selectedParty?.id}`);
                setCountMembers(response.data); 
            } catch (error) {
                console.error(error);
            }
        }
    
        if (selectedParty) {
            fetchPartyMembers(); 
        }
        
    }, [selectedParty]);
    

    const handleAddParty = async (values: PartyAffiliationType) => {
        try {
            const formData = new FormData();

            Object.entries(values).forEach(([key, value]) => {
                if (key !== 'logoImage' && value !== undefined) {
                    formData.append(key, value.toString());
                }
            });

            if (values.logoImage && values.logoImage.length > 0) {
                const file = values.logoImage[0].originFileObj;
                if (file) {
                    formData.append('logoImage', file);
                }
            }

            const response = await axios.post(CREATE_PARTY_URL, formData, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            });

            if (response.data.responseCode === 200) {
                const newParty = response.data.result;
                setParty(prevParty => [...prevParty, newParty]);

                toast.success('Successfully Created a Party.');
                setIsModalOpen(false);
            } else if (response.data.responseCode === 400) {
                toast.error('Create a party failed.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }

        } catch (error) {
            console.error(error);
        }
    };

    const handleDeleteParty = async (id: string) => {
        try {
            const response = await axios.delete(`${DELETE_PARTY_URL}${id}`);

            if (response.data.responseCode === 200) {
                toast.success('Successfully Deleted a Party');
                setFilteredParty(prevParty => prevParty.filter(party => party.id == id));
            } else if (response.data.responseCode === 400 || response.data.responseCode === 500) {
                toast.error('Failed to delete the party.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }
        } catch (error) {
            console.error(error);
        }
    };

    const handleSearchParty = async (searchQuery: string | null | undefined) => {
        if (searchQuery === null || searchQuery === undefined) {
            return setFilteredParty([]);
        }

        const trimmedQuery = searchQuery.trim();

        if (!trimmedQuery) {
            setFilteredParty(party);
            return;
        }

        const controller = new AbortController();
        const { signal } = controller;

        setIsLoading(true);
        try {
            const response = await axios.get(`${SEARCH_PARTY_URL}${encodeURIComponent(searchQuery)}`, { signal });
            if (response.data && !Array.isArray(response.data)) {
                setFilteredParty([response.data]);
            } else {
                setFilteredParty([]);
                toast.info('No party found matching the search criteria.');
            }
        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false);
        }

        return () => controller.abort();
    };

    const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === "Enter") {
            const value = (event.currentTarget as HTMLInputElement).value;
            handleSearchParty(value);
        } 
    };

    const handleUpdateParty = async (id: string, values: PartyAffiliationType) => {
        try {
            const formData = new FormData();

            Object.entries(values).forEach(([key, value]) => {
                if (key !== 'logoImage' && value !== undefined) {
                    formData.append(key, value.toString());
                }
            });

            if (values.logoImage && values.logoImage.length > 0) {
                const file = values.logoImage[0].originFileObj;
                if (file) {
                    formData.append('logoImage', file);
                }
            }

            const response = await axios.put(`${UPDATE_PARTY_URL}${id}`, formData, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            });

            if (response.data.responseCode === 200) {
                const updatedResponse = await axios.get(GET_ALL_PARTY);
                setParty(updatedResponse.data);
                setFilteredParty(updatedResponse.data);

                toast.success('Successfully Updated a Party.');
                setIsEditModalOpen(false);
            } else if (response.data.responseCode === 400) {
                toast.error('Create a party failed.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }

        } catch (error) {
            console.error(error);
        }
    };
    
    const columns = [
        {
            title: 'Image',
            dataIndex: 'logoImage',
            key: 'logoImage',
            render: (logoImage: string) => (
                logoImage ? (
                    <Image
                        src={`https://localhost:7196/${logoImage}`} 
                        width={50}
                        height={50}
                    />
                ) : (
                    <span>No Image Found</span>
                )
            )
        },
        {
            title: 'Party Name',
            dataIndex: 'partyName',
            key: 'partyName'
        },
        {
            title: 'No. of Members',
            dataIndex: 'members',
            key: 'members',
            render: () => countMembers || (selectedParty ? <Spin indicator={antIcon} /> : 0)
        },
        {
            title: 'Action',
            key: 'action',
            render: (record: PartyAffiliationType) => (
                <Space size='middle'>
                    <Button 
                        type='primary'
                        onClick={() => showEditModal(record as PartyAffiliationType)}
                    >
                        Edit
                    </Button>
                    <Popconfirm
                        title="Delete Ballot"
                        description="Are you sure you want to delete this ballot?"
                        onConfirm={() => handleDeleteParty(record.id as string)}
                        onCancel={handleCancel}
                        okText="Confirm"
                        cancelText="Cancel"
                    >
                        <Button type='primary' danger>Delete</Button>
                    </Popconfirm>
                </Space>
            )
        }
    ];  

    return (
        <React.Fragment>

            <div className="search-party-container">

                <Button className='button-addParty-content' onClick={showModal}>
                    Add Party
                </Button>

                <Search
                    className='search-party-content'
                    placeholder='Search...'
                    onSearch={(value: string) => handleSearchParty(value)}
                    size='large'
                    onKeyDown={handleKeyDown}
                />

            </div>

            <React.Fragment>
                {
                    isLoading ? (
                        <div className="loading-container">
                            <div className="loading-content">
                                <Spin indicator={antIcon} />
                            </div>
                        </div>
                    ) : (
                        <Table 
                            columns={columns} 
                            dataSource={filteredParty}
                            rowKey="id"
                            onRow={(record) => ({
                                onClick: () => setSelectedParty(record),
                            })}
                        />
                    )
                }
            </React.Fragment>

            {/* Add Modal */}

            <Modal
                title="Party"
                open={isModalOpen}
                onCancel={exitModal}
                centered
                footer={
                    <Space>
                        <Button onClick={exitModal}>
                            Cancel
                        </Button>
                        <Button form="create-party-form" type="primary" key='submit' htmlType="submit">
                            Submit
                        </Button>
                    </Space>
                }
                width={600}
            >

                <Form
                    initialValues={{ remember: true }}
                    id='create-party-form'
                    onFinish={handleAddParty}
                    onFinishFailed={onFinishFailed}
                    autoComplete="off"
                >
                    
                    <React.Fragment>
                        <Row gutter={16}>
                            <Col span={12}>
                                <div className='title-party-container'>
                                    <h4>Enter Party Name</h4>
                                </div>
                                <Form.Item<PartyAffiliationType>
                                    name='partyName'
                                    label='Party Name'
                                    rules={[{
                                        required: true,
                                        message: 'Please enter party name'
                                    }]}
                                >
                                    <Input
                                        placeholder='Party Name'
                                    />
                                </Form.Item>
                            </Col>
                        </Row>
                    </React.Fragment>

                    <React.Fragment>
                        <Row gutter={16}>
                            <Col span={12}>
                                <div className='image-title-container'>
                                    <h4>Please upload image</h4>
                                </div>
                                <Form.Item<PartyAffiliationType>
                                    name='logoImage'
                                    label='Logo Image'
                                    rules={[{ 
                                        required: true, 
                                        message: 'Please upload an image.' 
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
                    </React.Fragment>

                </Form>

            </Modal>

            {/* Edit Modal */}

            <Modal
                title="Party"
                open={isEditModalOpen}
                onCancel={exitEditModal}
                centered
                footer={
                    <Space>
                        <Button onClick={exitEditModal}>
                            Cancel
                        </Button>
                        <Button form="update-party-form" type="primary" key='submit' htmlType="submit">
                            Submit
                        </Button>
                    </Space>
                }
                width={600}
            >
                {
                    selectedParty && (
                        <Form
                            initialValues={{ remember: true }}
                            id='update-party-form'
                            onFinish={(values: PartyAffiliationType) => handleUpdateParty(selectedParty.id as string, values)}
                            onFinishFailed={onFinishFailed}
                            autoComplete="off"
                        >
                            <React.Fragment>
                                <Row gutter={16}>
                                    <Col span={12}>
                                        <div className='title-party-container'>
                                            <h4>Enter Party Name</h4>
                                        </div>
                                        <Form.Item<PartyAffiliationType>
                                            name='partyName'
                                            label='Party Name'
                                            initialValue={selectedParty.partyName}
                                            rules={[{
                                                required: true,
                                                message: 'Please enter party name'
                                            }]}
                                        >
                                            <Input
                                                placeholder='Party Name'
                                            />
                                        </Form.Item>
                                    </Col>
                                </Row>
                            </React.Fragment>

                            <React.Fragment>
                                <Row gutter={16}>
                                    <Col span={12}>
                                        <div className='image-title-container'>
                                            <h4>Please upload image</h4>
                                        </div>
                                        <Form.Item<PartyAffiliationType>
                                        name='logoImage'
                                        label='Logo Image'
                                        initialValue={selectedParty.logoImage ? [{ uid: '-1', name: selectedParty.logoImage, status: 'done', url: `https://localhost:7196/${selectedParty.logoImage}` }] : []}
                                        rules={[{ 
                                            required: true, 
                                            message: 'Please upload an image.' 
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
                            </React.Fragment>
                        </Form>
                    )
                }
                
            </Modal>

        </React.Fragment>
    )
}

export default Admin_Party