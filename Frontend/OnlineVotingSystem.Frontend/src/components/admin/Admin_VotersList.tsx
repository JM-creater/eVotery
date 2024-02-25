import React, { useEffect, useState } from 'react'
import '../admin/Admin_VotersList.css'
import axios from 'axios';
import { Button, Space, Table, Spin, Tooltip, Modal, Image, Form, Input, Switch } from 'antd';
import { LoadingOutlined, SearchOutlined } from '@ant-design/icons';
import { toast } from 'react-toastify';
import Search from 'antd/es/input/Search';

const GETALLVOTERS_URL = 'https://localhost:7196/User/get-all';
const GETBYVOTERSID_URL = 'https://localhost:7196/User/get-by-id/';
const VALIDATION_URL = 'https://localhost:7196/User/validate/';
const SEARCHVOTER_URL = 'https://localhost:7196/Search/search-voter?searchQuery=';
const ACTIVATE_URL = 'https://localhost:7196/User/activated/';
const DEACTIVATE_URL = 'https://localhost:7196/User/deactivated/';

type VoterType = {
    key?: string;
    voterId?: number;
    firstName?: string;
    lastName?: string;
    email?: string;
    address?: string;
    phoneNumber?: string;
    voterImages?: string;
    isActive?: boolean;
    isValidate?: boolean;
}

const Admin_VotersList: React.FC = () => {

    const [voters, setVoters] = useState<VoterType[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [filteredVoters, setFilteredVoters] = useState<VoterType[]>([]);
    const [selectedVoter, setSelectedVoter] = useState<VoterType | null>(null);
    const [isModalOpen, setIsModalOpen] = useState<boolean>(false);

    const antIcon = <LoadingOutlined style={{ fontSize: 50 }} spin />
    
    const handleOk = () => {
        setIsModalOpen(false);
    };
    
    const handleCancel = () => {
        setIsModalOpen(false);
    };

    useEffect(() => {
        const fetchVoters = async () => {
            try {
                const response = await axios.get(GETALLVOTERS_URL);
                setIsLoading(true);
                setVoters(
                    response.data.map(
                        (
                            row: {
                                voterId: VoterType;
                                firstName: VoterType;
                                lastName: VoterType;
                                email: VoterType;
                                address: VoterType;
                                phoneNumber: VoterType;
                                voterImages: VoterType;
                            }
                        ) => (
                            {
                                voterId: row.voterId,
                                firstName: row.firstName,
                                lastName: row.lastName,
                                email: row.email,
                                address: row.address,
                                phoneNumber: row.phoneNumber,
                                voterImages: row.voterImages,
                            }
                        )
                    )
                );
                setFilteredVoters(response.data);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
            }
        }
        
        fetchVoters();
    }, []);
    
    const columns = [
        {
            title: 'Image',
            dataIndex: 'voterImages',
            key: 'voterImages',
            render: (voterImages: VoterType) => (
                voterImages ? (
                    <Image 
                        src={`https://localhost:7196/${voterImages}`} 
                        width={50}
                        height={50}
                    />
                ) : (
                    <span>No Image Found</span>
                )
            )
        },
        {
            title: 'Voters ID',
            dataIndex: 'voterId',
            key: 'voterId'
        },
        {
            title: 'First Name',
            dataIndex: 'firstName',
            key: 'firstName',
        },
        {
            title: 'Last Name',
            dataIndex: 'lastName',
            key: 'lastName',
        },
        {
            title: 'Email',
            dataIndex: 'email',
            key: 'email',
        },
        {
            title: 'Phone Number',
            dataIndex: 'phoneNumber',
            key: 'phoneNumber',
        },
        {
            title: 'Action',
            key: 'action',
            render: (record: VoterType) => (
                <Space size="middle"> 

                    <Tooltip title="Info">
                        <Button 
                            type="primary" 
                            shape="circle" 
                            icon={<SearchOutlined />} 
                            onClick={() => handleFetchVotersById(record.voterId as number)}
                        />
                    </Tooltip>

                    <Button
                        type={
                                record.isValidate 
                                ? 'default' 
                                : 'primary'
                            }
                        onClick={() => handleValidate(record.voterId as VoterType)}
                        disabled={record.isValidate === true}
                    >    
                        {
                            record.isValidate 
                            ? 'Validated' 
                            : 'Pending Validation'
                        }
                    </Button>
                </Space>
            ),
        },
    ];

    const handleFetchVotersById = async (voterId: number) => {
        setIsModalOpen(true);

        try {
            const response = await axios.get(`${GETBYVOTERSID_URL}${voterId}`);

            if (response.status === 200) { 
                setSelectedVoter(response.data); 
            } else if (response.status === 404) {
                toast.error('Voters Id not found.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }
        } catch (error) {
            console.error(error);
        }
    };

    const handleValidate = async (voterId: VoterType) => {
        try {
            const response = await axios.put(`${VALIDATION_URL}${voterId}`);

            if (response.data.responseCode === 200) {
                const updatedVoters = voters.map(voter => {
                    if (voter.voterId === voterId) {
                        return { ...voter, isValidate: true }; 
                    }
                    return voter;
                });
                setVoters(updatedVoters);
                setFilteredVoters(updatedVoters.filter(voter => 
                    filteredVoters.some(
                        fv => fv.voterId === voter.voterId)
                    )
                );
            } else if (response.data.responseCode === 400) {
                toast.error('Validate Failed.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }

        } catch (error) {
            console.error(error);
        }
    };

    const handleSearchEnter = async (searchQuery: string) => {
        if (!searchQuery.trim()) {
            setFilteredVoters(voters);
            return;
        }
        setIsLoading(true); 
        try {
            const response = await axios.get(`${SEARCHVOTER_URL}${encodeURIComponent(searchQuery)}`);
            if (response.data && !Array.isArray(response.data)) {
                setFilteredVoters([response.data]);
            } else {
                setFilteredVoters([]);
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

    const handleActivate = async (voterId: VoterType) => {
        try {
            const response = await axios.put(`${ACTIVATE_URL}${voterId}`);

            if (response.data.responseCode === 200) {
                setSelectedVoter(prevVoter => ({ ...prevVoter, isActive: true }));
                toast.success('Account activated successfully.');
            } else if (response.data.responseCode === 400) {
                toast.error('Voters Id not found.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }

        } catch (error) {
            console.error(error);
        }
    };

    const handleDeactivate = async (voterId: VoterType) => {
        try {
            const response = await axios.put(`${DEACTIVATE_URL}${voterId}`);

            if (response.data.responseCode === 200) {
                setSelectedVoter(prevVoter => ({ ...prevVoter, isActive: false }));
                toast.success('Account deactivated successfully.');
            } else if (response.data.responseCode === 400) {
                toast.error('Voters Id not found.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }
            
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <React.Fragment>

            <div className="search-container">
                <Search 
                    className='search-content'
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
                        <Table columns={columns} dataSource={filteredVoters} />
                    )
                }
            </React.Fragment>
            
            <Modal 
                title="Voter's Information" 
                open={isModalOpen} 
                onOk={handleOk} 
                onCancel={handleCancel}
                width={1000}
                centered
            >
                <div className="modal-main-container">
                    {
                        selectedVoter && (

                            <React.Fragment>

                                <Image
                                    width={400}
                                    height={400}    
                                    src={
                                        `https://localhost:7196/${selectedVoter.voterImages}` 
                                    }
                                />

                                <div className="div-information-container">
                                    <Form>
                                        <Form.Item<VoterType>
                                            label="Voter ID:"
                                        >
                                            <Input value={selectedVoter.voterId} readOnly />
                                        </Form.Item>

                                        <Form.Item<VoterType>
                                            label="First Name:"
                                        >
                                            <Input value={selectedVoter.firstName} readOnly />
                                        </Form.Item>
                                        
                                        <Form.Item<VoterType>
                                            label="Last Name:"
                                        >
                                            <Input value={selectedVoter.lastName} readOnly />
                                        </Form.Item>

                                        <Form.Item<VoterType> 
                                            label="Email:"
                                        >
                                            <Input value={selectedVoter.email} readOnly />
                                        </Form.Item>

                                        <Form.Item<VoterType> 
                                            label="Address:"
                                        >
                                            <Input value={selectedVoter.address} readOnly />
                                        </Form.Item>

                                        <Form.Item<VoterType> 
                                            label="Phone Number:"
                                        >
                                            <Input value={selectedVoter.phoneNumber} readOnly />
                                        </Form.Item>

                                        <Form.Item<VoterType> 
                                            label={
                                                selectedVoter.isActive 
                                                ? 'Activated Account:' 
                                                : 'Deactivated Account:'
                                            }
                                        >
                                            <Switch 
                                                checked={selectedVoter?.isActive} 
                                                onChange={(checked: boolean) => 
                                                    checked 
                                                    ? handleActivate(selectedVoter.voterId as VoterType) 
                                                    : handleDeactivate(selectedVoter.voterId as VoterType)
                                                } 
                                            />
                                        </Form.Item>

                                    </Form>

                                </div>

                            </React.Fragment>

                        )
                    }
                    
                </div>
                
            </Modal>
            
        </React.Fragment>
    )
}

export default Admin_VotersList