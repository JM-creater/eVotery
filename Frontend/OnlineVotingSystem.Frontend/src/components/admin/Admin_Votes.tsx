import '../admin/Admin_Votes.css'
import React, { useEffect, useRef, useState } from 'react';
import { SearchOutlined } from '@ant-design/icons';
import type { InputRef, TableColumnsType, TableColumnType } from 'antd';
import { Button, Input, Space, Table } from 'antd';
import type { FilterDropdownProps } from 'antd/es/table/interface';
import Highlighter from "react-highlight-words";
import axios from 'axios';

const GET_VOTES_URL = 'https://localhost:7196/Votes/get-all';
const GET_USER_URL = 'https://localhost:7196/User/get-all';
const GET_CANDIDATE_URL = 'https://localhost:7196/Candidate/get-all';
const GET_POSITION_URL = 'https://localhost:7196/Position/get-all';

interface VotesType {
    key: string;
    userId: string;
    candidateId: string;
}

type UserType = {
    id?: string;
    firstName?: string;
    lastName?: string;
}

type CandidateType = {
    id?: string;
    firstName?: string;
    lastName?: string;
    positionId?: string;
}

type PositionType = {
    id?: string;
    name: string;
}

type DataIndex = keyof VotesType;

const Admin_Votes: React.FC = () => {

    const [position, setPosition] = useState<PositionType[]>([]);
    const [candidate, setCandidate] = useState<CandidateType[]>([]);
    const [user, setUser] = useState<UserType[]>([]);
    const [votes, setVotes] = useState<VotesType[]>([]);
    const [searchText, setSearchText] = useState('');
    const [searchedColumn, setSearchedColumn] = useState('');
    const searchInput = useRef<InputRef>(null);

    const getUserName = (userId: string) => {
        const foundUser = user.find(u => u.id === userId);
        return foundUser ? `${foundUser.firstName} ${foundUser.lastName}` : 'No User Found';
    };

    const getCandidateName = (candidateId: string) => {
        const candidateFound = candidate.find(c => c.id === candidateId);
        return candidateFound ? `${candidateFound.firstName} ${candidateFound.lastName}` : 'No Candidate Found';
    };

    const getPositionFromCandidate = (candidateId: string) => {
        const posFound = candidate.find(c => c.id === candidateId);
        return posFound ? posFound.positionId : null;
    };

    const getPositionName = (candidateId: string) => {
        const positionId = getPositionFromCandidate(candidateId);
        const positionFound = position.find(p => p.id === positionId);
        return positionFound ? positionFound.name : 'No Position Found';
    };

    useEffect(() => {
        const fetchVotes = async () => {
            try {
                const response = await axios.get(GET_VOTES_URL);
                setVotes(response.data);
            } catch (error) {
                console.error(error);
            }
        }

        fetchVotes();
    }, []);

    useEffect(() => {
        const fetchCandidates = async () => {
            try {
                const response = await axios.get(GET_CANDIDATE_URL);
                setCandidate(response.data);
            } catch (error) {
                console.error(error);
            }
        }

        fetchCandidates();
    }, []);

    useEffect(() => {
        const fetchUser = async () => {
            try {
                const response = await axios.get(GET_USER_URL);
                setUser(response.data);
            } catch (error) {
                console.error(error);
            }
        }

        fetchUser();
    }, []);

    useEffect(() => {
        const fetchPosition = async () => {
            try {
                const response = await axios.get(GET_POSITION_URL);
                setPosition(response.data);
            } catch (error) {
                console.error(error);
            }
        }

        fetchPosition();
    }, []);

    const handleSearch = (
        selectedKeys: string[],
        confirm: FilterDropdownProps['confirm'],
        dataIndex: DataIndex,
    ) => {
        confirm();
        setSearchText(selectedKeys[0]);
        setSearchedColumn(dataIndex);
    };

    const handleReset = (clearFilters: () => void) => {
        clearFilters();
        setSearchText('');
    };

    const getColumnSearchProps = (dataIndex: DataIndex): TableColumnType<VotesType> => ({
        filterDropdown: ({ setSelectedKeys, selectedKeys, confirm, clearFilters, close }) => (
            <div style={{ padding: 8 }} onKeyDown={(e) => e.stopPropagation()}>
                <Input
                    ref={searchInput}
                    placeholder={`Search ${dataIndex}`}
                    value={selectedKeys[0]}
                    onChange={(e) => setSelectedKeys(e.target.value ? [e.target.value] : [])}
                    onPressEnter={() => handleSearch(selectedKeys as string[], confirm, dataIndex)}
                    style={{ marginBottom: 8, display: 'block' }}
                />
                <Space>
                    <Button
                        type="primary"
                        onClick={() => handleSearch(selectedKeys as string[], confirm, dataIndex)}
                        icon={<SearchOutlined />}
                        size="small"
                        style={{ width: 90 }}
                    >
                        Search
                    </Button>
                    <Button
                        onClick={() => clearFilters && handleReset(clearFilters)}
                        size="small"
                        style={{ width: 90 }}
                    >
                        Reset
                    </Button>
                    <Button
                        type="link"
                        size="small"
                        onClick={() => {
                            confirm({ closeDropdown: false });
                            setSearchText((selectedKeys as string[])[0]);
                            setSearchedColumn(dataIndex);
                        }}
                    >
                        Filter
                    </Button>
                    <Button
                        type="link"
                        size="small"
                        onClick={() => {
                            close();
                        }}
                    >
                        close
                    </Button>
                </Space>
            </div>
        ),
        filterIcon: (filtered: boolean) => (
            <SearchOutlined style={{ color: filtered ? '#1677ff' : undefined }} />
        ),
        onFilter: (value, record) => {
            const positionName = getPositionName(record.candidateId);
            return positionName.toLowerCase().includes(value.toString().toLowerCase());
        },
        onFilterDropdownOpenChange: (visible) => {
            if (visible) {
                setTimeout(() => searchInput.current?.select(), 100);
            }
        },
        render: (record) =>
            searchedColumn === dataIndex ? (
                <Highlighter
                    highlightStyle={{ backgroundColor: '#ffc069', padding: 0 }}
                    searchWords={[searchText]}
                    autoEscape
                    textToHighlight={getPositionName(record.candidateId)}
                />
            ) : (
                getPositionName(record.candidateId)
            ),
        });
    
        const columns: TableColumnsType<VotesType> = [
            {
                title: 'Voter',
                dataIndex: 'userId',
                key: 'userId',
                width: '20%',
                ...getColumnSearchProps('userId'),
                render: (userId: UserType) => getUserName(userId as string)
            },
            {
                title: 'Candidate',
                dataIndex: 'candidateId',
                key: 'candidateId',
                width: '20%',
                ...getColumnSearchProps('candidateId'),
                render: (candidateId: CandidateType) => getCandidateName(candidateId as string)
            },
            {
                title: 'Position',
                dataIndex: 'candidateId',
                key: 'position',
                width: '20%',
                ...getColumnSearchProps('candidateId'),
                sorter: (a, b) => {
                    const positionNameA = getPositionName(a.candidateId);
                    const positionNameB = getPositionName(b.candidateId);
                    return positionNameA.localeCompare(positionNameB);
                },
                sortDirections: ['descend', 'ascend'],
                render: (candidateId: string) => getPositionName(candidateId as string)
            },
        ];

    return (
        <Table columns={columns} dataSource={votes} rowKey="id" />
    )
}

export default Admin_Votes