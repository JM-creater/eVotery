import { Button, Col, DatePicker, Form, Input, Modal, Popconfirm, Row, Space, Spin, Table } from 'antd'
import Search from 'antd/es/input/Search'
import React, { useEffect, useState } from 'react'
import '../admin/Admin_ElectionTitle.css'
import { LoadingOutlined } from '@ant-design/icons'
import axios from 'axios'
import { toast } from 'react-toastify'
import moment from 'moment'

const GETALL_ELECTION_URL = 'https://localhost:7196/Election/getall-elections';
const CREATE_ELECTION_URL = 'https://localhost:7196/Election/create-election';
const UPDATE_ELECTION_URL = 'https://localhost:7196/Election/update-election/'
const SEARCH_ELECTION_URL = 'https://localhost:7196/Search/search-election?searchQuery=';
const DELETE_ELECTION_URL = 'https://localhost:7196/Election/delete-election/';

type ElectionType = {
    id?: string;
    electionName?: string;
    startDate?: moment.Moment;
    endDate?: moment.Moment;
}

const Admin_ElectionTitle:React.FC = () => {

    const[election, setElection] = useState<ElectionType[]>([]);
    const[filteredElections, setFilteredElections] = useState<ElectionType[]>([]);
    const[isModalOpen, setIsModalOpen] = useState<boolean>(false);
    const[isEditModalOpen, setIsEditModalOpen] = useState<boolean>(false);
    const[isLoading, setIsLoading] = useState<boolean>(false);
    const[selectedElection, setSelectedElection] = useState<ElectionType | null>(null);
    const [form] = Form.useForm();

    const antIcon = <LoadingOutlined style={{ fontSize: 50 }} spin />

    useEffect(() => {
        const fetchElection = async () => {
            try {
                const response = await axios.get(GETALL_ELECTION_URL);
                setIsLoading(true);
                setElection(
                    response.data.map(
                        (
                            row: {
                                electionName: ElectionType;
                                startDate: ElectionType;
                                endDate: ElectionType;
                            }
                        ) => (
                            {
                                electionName: row.electionName,
                                startDate: row.startDate,
                                endDate: row.endDate,
                            }
                        )
                    )
                );
                setFilteredElections(response.data);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
            }
        }

        fetchElection();
    }, []);

    const showModal = () => {
        setIsModalOpen(true);
    };

    const exitModal = () => {
        form.resetFields();
        setIsModalOpen(false);
    };

    const showEditModal = (record: ElectionType) => {
        setSelectedElection(record);
        setIsEditModalOpen(true);
    };

    const exitEditModal = () => {
        form.resetFields();
        setIsEditModalOpen(false);
    };


    const formatDate = (dateString: string) => {
        const date = new Date(dateString);
        const day = String(date.getDate()).padStart(2, '0');
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const year = date.getFullYear();
        return `${month}/${day}/${year}`;
    };

    const onFinishFailed = (errorInfo: unknown) => {
        console.log('Failed:', errorInfo);
    };

    const handleCancel = (e?: React.MouseEvent<HTMLElement, MouseEvent>) => {
        console.log(e);
    };

    const columns = [
        {
            title: 'Election',
            dataIndex: 'electionName',
            key: 'electionName'
        },
        {
            title: 'Start Date',
            dataIndex: 'startDate',
            key: 'startDate',
            render: (datestring: string) => formatDate(datestring as string)
        },
        {
            title: 'End Date',
            dataIndex: 'endDate',
            key: 'endDate',
            render: (datestring: string) => formatDate(datestring as string)
        },
        {
            title: 'Action',
            key: 'action',
            render: (record: ElectionType) => (
                <Space size='middle'>
                    <Button 
                        type='primary'
                        onClick={() => showEditModal(record as ElectionType)}
                    >
                        Edit
                    </Button>
                    <Popconfirm
                        title="Delete Ballot"
                        description="Are you sure you want to delete this ballot?"
                        onConfirm={() => handleDeleteElection(record.id as string)}
                        onCancel={handleCancel}
                        okText="Confirm"
                        cancelText="Cancel"
                    >
                        <Button type='primary' danger>Delete</Button>
                    </Popconfirm>
                </Space>
            )
        },
    ];

    const handleCreateElection = async (values: ElectionType) => {
        try {

            const response = await axios.post(CREATE_ELECTION_URL, values, {
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            if (response.data.responseCode === 200) {
                form.resetFields();
                const newElection = response.data.result;
                setElection(prevElections => [...prevElections, newElection]);
                setFilteredElections(prevElections => [...prevElections, newElection]);
                setIsModalOpen(false);
            } else if (response.data.responseCode === 400) {
                toast.error('Create a election failed.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }

        } catch (error) {
            console.error(error);
        }
    };

    const handleUpdateElection = async (id: string, values: ElectionType) => {
        try {
            const updatedValues = {
                ...values,
                startDate: values.startDate ? values.startDate.format('YYYY-MM-DDTHH:mm:ss') : null,
                endDate: values.endDate ? values.endDate.format('YYYY-MM-DDTHH:mm:ss') : null
            };
    
            const response = await axios.put(`${UPDATE_ELECTION_URL}${id}`, updatedValues, {
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            if (response.data.responseCode === 200) {
                const updatedResponse = await axios.get(GETALL_ELECTION_URL);
                setElection(updatedResponse.data);
                setFilteredElections(updatedResponse.data);
                setIsEditModalOpen(false);
            } else if (response.data.responseCode === 400) {
                toast.error('Update a election failed.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }

        } catch (error) {
            console.error(error);
        }
    };

    const handleDeleteElection = async (id: string) => {
        try {
            const response = await axios.delete(`${DELETE_ELECTION_URL}${id}`);

            if (response.data.responseCode === 200) {
                toast.success('Successfully Deleted a Election');
                setFilteredElections(prevElections =>  prevElections.filter(election => election.id == id));
            } else if (response.data.responseCode === 400 || response.data.responseCode === 500) {
                toast.error('Failed to delete the election.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }
            
        } catch(error) {
            console.error(error);
        }
    };

    const handleSearchEnter = async (searchQuery: string | null | undefined) => {
        if (searchQuery === null || searchQuery === undefined) {
            return setFilteredElections([]);
        }
        
        const trimmedQuery = searchQuery.trim();

        if (!trimmedQuery) {
            setFilteredElections(election);
            return;
        }

        const controller = new AbortController();
        const { signal } = controller;

        setIsLoading(true); 
        try {
            const response = await axios.get(`${SEARCH_ELECTION_URL}${encodeURIComponent(searchQuery)}`, { signal });
            if (response.data && !Array.isArray(response.data)) {
                setFilteredElections([response.data]);
            } else {
                setFilteredElections([]);
                toast.info('No election found matching the search criteria.');
            }
        } catch (error) {
            if (axios.isAxiosError(error) && error.name !== 'AbortError') {
                console.error(error);
            }
        } finally {
            setIsLoading(false); 
        }

        return () => controller.abort();
    };
    
    const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === "Enter") {
            const value = (event.currentTarget as HTMLInputElement).value;
            handleSearchEnter(value);
        }
    };

    return (
        <React.Fragment>

            <div className='search-election-container'>

                <Button className='button-addElection-content' onClick={showModal}>
                    Add Election
                </Button>

                <Search 
                    className='search-election-content'
                    placeholder='Search...' 
                    onSearch={(value: string) => handleSearchEnter(value)}
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
                        <Table columns={columns} dataSource={filteredElections} rowKey="id" />
                    )
                }
            </React.Fragment>

            {/* Add Modal */}
            
            <Modal
                title="Ballot"
                open={isModalOpen}
                onCancel={exitModal}
                centered
                footer={
                    <Space>
                        <Button onClick={exitModal}>
                            Cancel
                        </Button>
                        <Button form="create-election-form" type="primary" key='submit' htmlType="submit">
                            Submit
                        </Button>
                    </Space>
                }
                width={800}
                maskClosable={false}
            >

                <Form
                    form={form}
                    id="create-election-form" 
                    onFinish={handleCreateElection}
                    onFinishFailed={onFinishFailed}
                >
                    
                    <React.Fragment>
                        <Row gutter={16}>
                            <Col span={12}>
                                <div className="title-election-container">
                                    <h4>Enter Ballot Name</h4>
                                </div>
                                <Form.Item<ElectionType>
                                    name='electionName'
                                    label='Election Name'
                                    rules={[{ 
                                        required: true, 
                                        message: 'Please enter election name' 
                                    }]}
                                >
                                    <Input
                                        placeholder='Election Name'
                                    />
                                </Form.Item>
                            </Col>
                        </Row>
                    </React.Fragment>
                    
                    <React.Fragment>
                        <div className="date-election-container">
                            <h4>Please select the start and end dates</h4>
                        </div>
                        
                        <Row gutter={16}>
                            <Col span={9}>
                                <Form.Item<ElectionType>
                                name='startDate'
                                label='Start Date'
                                rules={[{ 
                                    required: true, 
                                    message: 'Please enter a starting date' 
                                }]}
                                >
                                    <DatePicker 
                                        placeholder='Start Date'
                                    />
                                </Form.Item>
                                
                            </Col>
                            <Col span={9}>
                                <Form.Item<ElectionType>
                                    name='endDate'
                                    label='End Date'
                                    rules={[{ 
                                        required: true, 
                                        message: 'Please enter a ending date' 
                                    }]}
                                >
                                    <DatePicker 
                                        placeholder='End Date'
                                    />
                                </Form.Item>
                            </Col>
                        </Row>
                    </React.Fragment>
                    
                </Form>
            </Modal>

            {/* Edit Modal */}

            <Modal
                title="Edit Election"
                open={isEditModalOpen}
                onCancel={exitEditModal}
                centered
                footer={
                    <Space>
                        <Button onClick={exitEditModal}>
                            Cancel
                        </Button>
                        <Button form="update-election-form" type="primary" key='submit' htmlType="submit">
                            Update
                        </Button>
                    </Space>
                }
                width={800}
            >
                {
                    selectedElection && (
                        <Form
                            id="update-election-form" 
                            onFinish={(values: ElectionType) => handleUpdateElection(selectedElection.id as string, values)}
                            onFinishFailed={onFinishFailed}
                        >
                            
                            <React.Fragment>
                                <Row gutter={16}>
                                    <Col span={12}>
                                        <div className="title-election-container">
                                            <h4>Enter Ballot Name</h4>
                                        </div>
                                        <Form.Item<ElectionType>
                                            name='electionName'
                                            label='Election Name'
                                            initialValue={selectedElection.electionName}
                                            rules={[{ 
                                                required: true, 
                                                message: 'Please enter election name' 
                                            }]}
                                        >
                                            <Input
                                                placeholder='Election Name'
                                            />
                                        </Form.Item>
                                    </Col>
                                </Row>
                            </React.Fragment>
                            
                            <React.Fragment>
                                <div className="date-election-container">
                                    <h4>Please select the start and end dates</h4>
                                </div>
                                
                                <Row gutter={16}>
                                    <Col span={9}>
                                        <Form.Item<ElectionType>
                                        name='startDate'
                                        label='Start Date'
                                        initialValue={selectedElection.startDate ? moment(selectedElection.startDate) : null} 
                                        rules={[{ 
                                            required: true, 
                                            message: 'Please enter a starting date' 
                                        }]}
                                        >
                                            <DatePicker 
                                                placeholder='Start Date'
                                            />
                                        </Form.Item>
                                        
                                    </Col>
                                    <Col span={9}>
                                        <Form.Item<ElectionType>
                                            name='endDate'
                                            label='End Date'
                                            initialValue={selectedElection.endDate ? moment(selectedElection.endDate) : null}
                                            rules={[{ 
                                                required: true, 
                                                message: 'Please enter a ending date' 
                                            }]}
                                        >
                                            <DatePicker 
                                                placeholder='End Date'
                                            />
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

export default Admin_ElectionTitle