import { LoadingOutlined } from '@ant-design/icons';
import { 
    Button, 
    Col, 
    DatePicker, 
    Form, 
    Input, 
    Modal, 
    Popconfirm, 
    Row, 
    Select, 
    Space, 
    Spin, 
    Table 
} from 'antd';
import Search from 'antd/es/input/Search';
import axios from 'axios';
import moment from 'moment';
import React, { useEffect, useState } from 'react'
import '../admin/Admin_BallotPosition.css';
import { toast } from 'react-toastify';

const GETBALLOT_URL = 'https://localhost:7196/Ballot/getall-ballots';
const CREATE_BALLOT_URL = 'https://localhost:7196/Ballot/create-ballot';
const DELETE_BALLOT_URL = 'https://localhost:7196/Ballot/delete-ballot/'
const GETALLELECTION_URL = 'https://localhost:7196/Election/getall-elections';
const SEARCHBALLOT_URL = 'https://localhost:7196/Search/search-ballot?searchQuery='

type BallotType = {
    id?: string;
    ballotName?: string;
    electionId?: string;
    startDate?: moment.Moment;
    endDate?: moment.Moment;
}

type ElectionType = {
    id?: string;
    electionName?: string;
}

const Admin_BallotPosition:React.FC = () => {

    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [ballot, setBallot] = useState<BallotType[]>([]);
    const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
    const [election, setElection] = useState<ElectionType[]>([]);

    const antIcon = <LoadingOutlined style={{ fontSize: 50 }} spin />

    const delay = (ms: number | undefined) => new Promise(resolve => setTimeout(resolve, ms));

    useEffect(() => {
        const fetchBallot = async () => {
            try {
                const response = await axios.get(GETBALLOT_URL);
                setIsLoading(true);
                setBallot(response.data);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
            }
        }

        fetchBallot();
    }, []);

    useEffect(() => {
        const fetchElection = async () => {
            try {
                const response = await axios.get(GETALLELECTION_URL);
                setElection(response.data);
            } catch (error) {
                console.error(error);
            }
        }

        fetchElection();
    }, []);

    const formatDate = (dateString: string) => {
        const date = new Date(dateString);
        const day = String(date.getDate()).padStart(2, '0');
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const year = date.getFullYear();
        return `${month}/${day}/${year}`;
    };

    const showModal = () => {
        setIsModalOpen(true);
    };

    const exitModal = () => {
        setIsModalOpen(false);
    };

    const onFinishFailed = (errorInfo: unknown) => {
        console.log('Failed:', errorInfo);
    };

    const handleCancel = (e?: React.MouseEvent<HTMLElement, MouseEvent>) => {
        console.log(e);
    };

    const columns = [
        {
            title: 'Ballot Name',
            dataIndex: 'ballotName',
            key: 'ballotName'
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
            render: (record: BallotType) => (
                <Space size='middle'>
                    <Button type='primary'>Edit</Button>
                    <Popconfirm
                        title="Delete Ballot"
                        description="Are you sure you want to delete this ballot?"
                        onConfirm={() => handleDeleteBallot(record.id as string)}
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

    const handleCreateBallot = async (values: BallotType) => {
        try {

            const response = await axios.post(CREATE_BALLOT_URL, values, {
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            if (response.data.responseCode === 200) {
                toast.success('Successfully Created a Ballot.');
                setIsModalOpen(false);
                await delay(100);
                window.location.reload();
            } else if (response.data.responseCode === 400) {
                toast.error('Create a ballot failed.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }

        } catch (error) {
            console.error(error);
        }
    };

    const handleDeleteBallot = async (id: string) => {
        try {
            const response = await axios.delete(`${DELETE_BALLOT_URL}${id}`);

            if (response.data.responseCode === 200) {
                toast.success('Successfully Deleted a Ballot');
                const updatedElections = election.filter(p => p.id !== id);
                setBallot(updatedElections);
            } else if (response.data.responseCode === 400) {
                toast.error('Failed to delete the ballot.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }
            
        } catch(error) {
            console.error(error);
        }
    };

    const handleSearchEnter = async (searchQuery: string) => {
        if (!searchQuery.trim()) {
            setBallot(election);
            return;
        }
        setIsLoading(true); 
        try {
            const response = await axios.get(`${SEARCHBALLOT_URL}${encodeURIComponent(searchQuery)}`);
            if (response.data && !Array.isArray(response.data)) {
                setBallot([response.data]);
            } else {
                setBallot([]);
                toast.info('No voters found matching the search criteria.');
            }
        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false); 
        }
    };
    
    const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === "Enter") {
            const value = (event.currentTarget as HTMLInputElement).value;
            handleSearchEnter(value);
        }
    };

    return (
        <React.Fragment>

            <div className="search-ballot-container">

                <Button className='button-addBallot-content' onClick={showModal}>
                    Add Ballot
                </Button>

                <Search 
                    className='search-ballot-content'
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
                        <Table columns={columns} dataSource={ballot} />
                    )
                }
            </React.Fragment>
            
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
                        <Button form="create-candidate-form" type="primary" key='submit' htmlType="submit">
                            Submit
                        </Button>
                    </Space>
                }
                width={800}
            >

                <Form
                    id="create-candidate-form" 
                    onFinish={handleCreateBallot}
                    onFinishFailed={onFinishFailed}
                >

                    <React.Fragment>
                        <Row gutter={16}>
                            <Col span={12}>
                                <div className="name-title-container">
                                    <h4>Enter Ballot Name</h4>
                                </div>
                                <Form.Item<BallotType>
                                    name='ballotName'
                                    label='Ballot Name'
                                    rules={[{ 
                                        required: true, 
                                        message: 'Please enter ballot name' 
                                    }]}
                                >
                                    <Input
                                        placeholder='Ballot Name'
                                    />
                                </Form.Item>
                            </Col>
                            <Col span={12}>
                                <div className="name-title-container">
                                    <h4>Select Election</h4>
                                </div>
                                <Form.Item<BallotType>
                                    name='electionId'
                                    label='Election'
                                    rules={[{ 
                                        required: true, 
                                        message: 'Please select a election' 
                                    }]}
                                >
                                    <Select
                                        placeholder="Election"
                                    >
                                        {
                                            election.map(
                                                elections => (
                                                    <Select.Option
                                                        key={elections.id}
                                                        value={elections.id}
                                                    >
                                                        {elections.electionName}
                                                    </Select.Option>
                                                )
                                            )
                                        }
                                    </Select>
                                </Form.Item>
                            </Col>
                        </Row>
                    </React.Fragment>
                    
                    <React.Fragment>
                        <div className="date-title-container">
                            <h4>Please select the start and end dates</h4>
                        </div>
                        
                        <Row gutter={16}>
                            <Col span={9}>
                                <Form.Item<BallotType>
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
                                <Form.Item<BallotType>
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

        </React.Fragment>
        
    )
}

export default Admin_BallotPosition