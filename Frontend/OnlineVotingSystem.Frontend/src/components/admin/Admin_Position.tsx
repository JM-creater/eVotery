import { CloseOutlined, LoadingOutlined } from '@ant-design/icons';
import { Button, Input, Modal, Popconfirm, Space, Spin, Table } from 'antd';
import Search from 'antd/es/input/Search';
import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { toast } from 'react-toastify';
import '../admin/Admin_Position.css'

const GETALLPOSITION_URL = 'https://localhost:7196/Position/get-all';
const SEARCHPOSITION_URL = 'https://localhost:7196/Search/search-position?searchQuery=';
const GETCOUNTCANDIDATES_URL = 'https://localhost:7196/Position/get-count-by-name';
const ADD_POSITION_URL = 'https://localhost:7196/Position/create-position?name=';
const UPDATE_POSITION_URL = 'https://localhost:7196/Position/update-position/';
const DELETE_POSITION_URL = 'https://localhost:7196/Position/delete-position/';

type PositionType = {
    id?: string;
    name?: string; 
    isActive?: boolean;
}

const Admin_Position: React.FC = () => {

    const [positions, setPositions] = useState<PositionType[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [filteredPosition, setFilteredPosition] = useState<PositionType[]>([]);
    const [countCandidates, setCountCandidates] = useState<{ [key: string]: number }>({});
    const [changeInput, setChangeInput] = useState<string | null>(null);
    const [inputValue, setInputValue] = useState<string>('');
    const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
    const [newPosition, setNewPosition] = useState<string>('');

    const antIcon = <LoadingOutlined style={{ fontSize: 50 }} spin />

    const delay = (ms: number | undefined) => new Promise(resolve => setTimeout(resolve, ms));

    useEffect(() => {
        const fetchPositions = async () => {
            try {
                const response = await axios.get(GETALLPOSITION_URL);
                setIsLoading(true);
                setPositions(
                    response.data.map(
                        (
                            row: {
                                name: PositionType;
                            }
                        ) => (
                            {
                                name: row.name
                            }
                        )
                    )
                );
                setFilteredPosition(response.data);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
            }
        }

        fetchPositions();
    }, []);

    useEffect(() => {
        const fetchCountCandidates = async () => {
            try {
                const response = await axios.get(GETCOUNTCANDIDATES_URL);
                setCountCandidates(response.data);
            } catch (error) {
                console.error(error);
            }
        }

        fetchCountCandidates();
    }, []);

    const columns = [
        {
            title: 'Position Name',
            dataIndex: 'name',
            key: 'name',
            render: (text: string, record: PositionType) => (
                changeInput === record.name ? (
                    <React.Fragment>
                        <div className="input-div-container">
                            <Input
                                value={inputValue}
                                onChange={(e) => setInputValue(e.target.value)}
                                onPressEnter={() => handleEditConfirm(record.id as string)}
                                onBlur={() => handleEditConfirm(record.id as string)}
                            />
                            
                            <CloseOutlined onClick={closeInput} className='close-outline-div' /> 
                        </div>
                    </React.Fragment>
                ) : (
                    text
                )
            )
        },
        {
            title: 'No. of Candidates',
            dataIndex: 'name',
            key: 'noOfCandidates',
            render: (name: string) => (
                countCandidates[name] || 0
            )
        },
        {
            title: 'Active Position',
            dataIndex: 'isActive',
            key: 'isActive',
            render: (isActive: boolean) => (
                <span>{isActive ? 'Active' : 'Not Active'}</span>
            )
        },
        {
            title: 'Action',
            key: 'action',
            render: (record: PositionType) => (
                <Space size='middle'>
                    <Button 
                        type='primary'
                        onClick={() => onClickChange(record.name as string)}
                    > 
                        Edit
                    </Button>

                    <Popconfirm
                        title="Delete Position"
                        description="Are you sure you want to delete this position?"
                        onConfirm={() => handleDeletePosition(record.id as string)}
                        onCancel={handleCancel}
                        okText="Confirm"
                        cancelText="Cancel"
                    >
                        <Button type='primary' danger>
                            Delete
                        </Button>
                    </Popconfirm>
                    
                </Space>
            )
        },
    ];

    const closeInput = () => {
        setChangeInput(null);
    };

    const showModal = () => {
        setIsModalOpen(true);
    };

    const handleExit = () => {
        setIsModalOpen(false);
    };

    const handleCancel = (e?: React.MouseEvent<HTMLElement, MouseEvent>) => {
        console.log(e);
    };

    const handleAddPosition = async () => {
        if (!newPosition.trim()) {
            toast.error('Position name cannot be empty.');
            return;
        }

        try {
            
            const response = await axios.post(`${ADD_POSITION_URL}${encodeURIComponent(newPosition)}`);

            if (response.data.responseCode === 200) {
                toast.success('Successfully Add a Position.');
                setIsModalOpen(false);
                setNewPosition(''); 

                setPositions(prevPositions => [
                    ...prevPositions,
                    {
                        name: newPosition,
                    }
                ]);

                setFilteredPosition(prevPositions => [
                    ...prevPositions,
                    {
                        name: newPosition,
                    }
                ]);
            } else if (response.data.responseCode === 400) {
                toast.error('Failed to delete the position.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }

        } catch (error) {
            console.error(error);
        }
    };

    const handleEditConfirm = async (id: string) => {

        if (inputValue.trim() === '') {
            toast.error('Position name cannot be empty.');
            return;
        }

        try {
            const position = positions.find(pos => pos.name === changeInput);

            if (!position) {
                toast.error('Position not found.');
                return;
            }

            const updatedPosition = { 
                name: inputValue 
            };

            const response = await axios.put(`${UPDATE_POSITION_URL}${id}`, updatedPosition, {
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            if (response.data.responseCode === 200) {
                const updatedPositions = positions.map(pos => {
                    if (pos.id === position.id) {
                        return { ...pos, name: inputValue };
                    }
                    return pos;
                });
                
                setPositions(updatedPositions);
                setChangeInput(null);
                toast.success('Position name updated successfully.');
                await delay(100);
                window.location.reload();
            } else {
                toast.error('Failed to update position name.');
            }
        } catch (error) {
            console.error(error);
        }
    };

    const onClickChange = (name: string) => {
        setChangeInput(name);
        const position = positions.find(pos => pos.name === name);
        setInputValue(position?.name || '');
    };

    const handleSearchEnter = async (searchQuery: string | null | undefined) => {
        if (searchQuery === null || searchQuery === undefined || searchQuery.trim() === '') {
            return setFilteredPosition([]);
        }

        const trimmedQuery = searchQuery.trim();

        if (!trimmedQuery) {
            setFilteredPosition(positions);
            return;
        }

        const controller = new AbortController();
        const { signal } = controller;

        setIsLoading(true); 
        try {
            const response = await axios.get(`${SEARCHPOSITION_URL}${encodeURIComponent(searchQuery)}`, { signal });
            if (response.data && !Array.isArray(response.data)) {
                setFilteredPosition([response.data]);
            } else {
                setFilteredPosition([]);
                toast.info('No voters found matching the search criteria.');
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

    const handleDeletePosition = async (id: string) => {
        try {   
            const response = await axios.delete(`${DELETE_POSITION_URL}${id}`);

            if (response.data.responseCode === 200) {
                toast.success('Successfully Deleted a Position');

                const updatedPositions = positions.filter(p => p.id !== id);
                setPositions(updatedPositions);

                const updatedFilteredPositions = filteredPosition.filter(fp => fp.id !== id);
                setFilteredPosition(updatedFilteredPositions);
            } else if (response.data.responseCode === 400) {
                toast.error('Failed to delete the position.');
            } else {
                toast.error('An error occurred. Please try again later.');
            }

        } catch (error) {
            console.error(error);
        }
    };

    return (
        <React.Fragment>

            <div className="search-position-container">

                <Button className='button-add-content' onClick={showModal}>
                    Add Position
                </Button>

                <Search 
                    className='search-position-content'
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
                        <Table columns={columns} dataSource={filteredPosition} rowKey="id" />
                    )
                }
            </React.Fragment>

            <Modal 
                title="Add Position" 
                open={isModalOpen}
                onOk={handleAddPosition}
                onCancel={handleExit}
                centered
            >
                <Input
                    value={newPosition}
                    onChange={(e) => setNewPosition(e.target.value)}
                    placeholder='Position Name'
                />
            </Modal>

        </React.Fragment>
    )
}

export default Admin_Position