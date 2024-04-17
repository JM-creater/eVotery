import React, { useEffect, useState } from 'react'
import '../voter/Voter_ViewResponses.css'
import { 
    Badge, 
    Button, 
    Checkbox, 
    Collapse, 
    Descriptions, 
    DescriptionsProps, 
    Flex, 
    Modal, 
    Space, 
    Typography 
} from 'antd';
import { CaretRightOutlined, SearchOutlined } from '@ant-design/icons';
import axios from 'axios';
import Voter_HomePage from './Voter_HomePage';

type CandidateType = {
    id: string;
    firstName?: string;
    lastName?: string;
    gender?: string;
    image?: string;
    positionId?: string;
    partyAffiliationId?: string;
    biography?: string;
    status?: CandidateStatus;
    ballotId?: string;
    votes?: VotesType[];
    currentUserVoted?: boolean;
}

type PositionType = {
    id: string;
    name?: string;
    candidates: CandidateType[];
}

type PartyAffiliationType = {
    id?: string;
    partyName?: string;
}

type BallotType = {
    id?: string;
    ballotName?: string;
}

type VotesType = {
    id?: string;
    userId?: string;
    candidateId?: string;
}

enum CandidateStatus {
    Active = 1,
    InActive = 2,
    Disqualified = 3
}

const GETAll_POSITION_URL = 'https://localhost:7196/Position/get-all';
const GET_CANDIDATES_BYID = 'https://localhost:7196/Candidate/';
const GET_PARTYAFFILATION_URL = 'https://localhost:7196/PartyAffiliation/getall-party';
const GET_BALLOT_URL = 'https://localhost:7196/Ballot/getall-ballots';
const GET_COUNTVOTES_URL = 'https://localhost:7196/Candidate/count-votes/';

const Voter_ViewResponses: React.FC = () => {

    const [position, setPosition] = useState<PositionType[]>([]);
    const [activeKeys, setActiveKeys] = useState<string[]>([]);
    const [isLoading, ] = useState<boolean>(false);
    const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
    const [selectedCandidate, setSelectedCandidate] = useState<CandidateType | null>(null);
    const [partyAffiliation, setPartyAffiliation] = useState<PartyAffiliationType[]>([]);
    const [ballot, setBallot] = useState<BallotType[]>([]);
    const [countVotes, setCountVotes] = useState<number>(0);
    const [selectedMenuItem, setSelectedMenuItem] = useState<number>(1);
    
    useEffect(() => {
        const fetchPosition = async () => {

            try {
                const response = await axios.get(GETAll_POSITION_URL);
                setPosition(response.data);

                const positionIds = response.data.map((pos: PositionType) => pos.id);
                setActiveKeys(positionIds);
            } catch (error) {
                console.error(error);
            }
        }
    
        fetchPosition();
    }, []);
    
    useEffect(() => {
        const fetchParty = async () => {
            try {
                const response = await axios.get(GET_PARTYAFFILATION_URL);
                setPartyAffiliation(response.data);
            } catch (error) {
                console.error(error);
            }
        }
    
        fetchParty();
    }, []);
    
    useEffect(() => {
        const fetchBallot = async () => {
            try {
                const response = await axios.get(GET_BALLOT_URL);
                setBallot(response.data);
            } catch (error) {
                console.error(error);
            }
        }
    
        fetchBallot();
    }, []);

    const handleSelectedMenu = () => {
        setSelectedMenuItem(2);
    };

    const getCandidateById = async (id: string) => {
        try {
            const response = await axios.get(`${GET_CANDIDATES_BYID}${id}`);
            setSelectedCandidate(response.data);
        } catch (error) {
            console.error(error);
        }
    };
    
    const showModal = (id: string) => {
        setIsModalOpen(true);
        getCandidateById(id);
        getCountVotes(id);
    };
    
    const exitModal = () => {
        setIsModalOpen(false);
    };

    const getPartyAffiliationName = (partyId: string) => {
        const party = partyAffiliation.find(p => p.id === partyId);
        return party ? party.partyName : 'No Party Found';
    };
    
    const getPositionName = (positionId: string) => {
        const pos = position.find(p => p.id === positionId);
        return pos ? pos.name : 'No Position Found';
    };
    
    const getBallotName = (ballotId: string) => {
        const bal = ballot.find(b => b.id === ballotId);
        return bal ? bal.ballotName : 'No Ballot Found';
    };
    
    const getCountVotes = async (id: string) => {
        try {
            const response = await axios.get(`${GET_COUNTVOTES_URL}${id}`);
            setCountVotes(response.data);
        } catch (error) {
            console.error(error);
        }
    };
    
    const descriptionItems: DescriptionsProps['items'] = [
        {
            key: '1',
            label: 'First Name',
            children: (
                selectedCandidate?.firstName
            ),
        },
        {
            key: '2',
            label: 'Last Name',
            children: (
                selectedCandidate?.lastName
            ),
        },
        {
            key: '3',
            label: 'Gender',
            children: (
                selectedCandidate?.gender === '1' ? (
                    <p>Female</p>
                ) : (
                    <p>Male</p>
                )
            ),
            },
        {
            key: '4',
            label: 'Party Affiliate',
            children: (
                getPartyAffiliationName(selectedCandidate?.partyAffiliationId as string)
            ),
        },
        {
            key: '5',
            label: 'Position',
            span: 2,
            children: (
                getPositionName(selectedCandidate?.positionId as string)
            ),
        },
        {
            key: '6',
            label: 'Biography',
            span: 3,
            children: (
                selectedCandidate?.biography
            ),
        },
        {
            key: '7',
            label: 'Status',
            children: (
                <Badge 
                    status={selectedCandidate?.status === CandidateStatus.Active ? (
                            'success'
                        ) : (
                            'error'
                        )
                    } 
                    text={selectedCandidate?.status === CandidateStatus.Active ? (
                            'Active'
                        ) : (
                            'Not Active'
                        )
                    } 
                />
            ),
        },
        {
            key: '8',
            label: 'Election Ballot',
            children: (
                getBallotName(selectedCandidate?.ballotId as string)
            ),
        },
        {
            key: '9',
            label: 'No. of Votes',
            children: (
                countVotes ?? 0
            ),
        },
        {
            key: '10',
            label: 'Candidate Image',
            children: (
                <React.Fragment>
                    <img 
                        src={`https://localhost:7196/${selectedCandidate?.image}`} 
                        alt="image-candidate" 
                        width={150}
                        style={{ maxWidth: '100%' }}
                    />
                </React.Fragment>
            ),
        },
    ];
    
    const items = position.map(pos => ({
        key: pos.id,
        label: <div className="response-position-label"><CaretRightOutlined />{pos.name}</div>, 
        showArrow: false,
        children: pos.candidates.length > 0 ? (
        <React.Fragment>
            <div className="response-candidate-container">
                {
                    pos.candidates
                    .filter(cand => cand.status === CandidateStatus.InActive ? 
                                    !CandidateStatus.Active :
                                    CandidateStatus.Disqualified 
                            )
                    .map(cand => (
                        <div className='response-candidate-select' key={cand.id}> 
                            <Flex
                                key={cand.id}
                                justify='flex-start'
                                align='center'
                                gap='large'
                            >
                                <React.Fragment>
                                    <Checkbox 
                                        checked={cand.currentUserVoted } 
                                    />
                                    <img
                                        src={`https://localhost:7196/${cand.image}`}
                                        alt={`${cand.firstName} ${cand.lastName}`}
                                        width={150}
                                        style={{ maxWidth: '100%' }}
                                    />
                                    <Flex align='center' vertical>
                                        <h1>{`${cand.firstName} ${cand.lastName}`}</h1>
                                        <Button 
                                            size='small' 
                                            type="primary" 
                                            icon={<SearchOutlined />}
                                            onClick={() => showModal(cand.id as string)}
                                        >
                                            Read More
                                        </Button>
                                    </Flex>
                                </React.Fragment>
                            </Flex>
                        </div>
                    ))
                }
            </div>
        </React.Fragment>
        ) : (
            <Flex justify='center' align='center'>
                <p>No candidates for this position.</p>
            </Flex>
        )
    }));

    return (
        <React.Fragment>
            {
                selectedMenuItem === 1 ? (
                    <React.Fragment>
                        <React.Fragment>
                            <Flex justify='center' align='center'>
                                <Typography.Title level={1}>
                                    ELECTION 2024
                                </Typography.Title>
                            </Flex>

                            <Collapse items={items} activeKey={activeKeys} />

                            <Flex justify='center' align='center'>
                                <Button
                                    size='large'
                                    style={{  width: '40vh', margin: 30 }}
                                    type='primary'
                                    loading={isLoading}
                                    onClick={handleSelectedMenu}
                                >
                                    Home
                                </Button>
                            </Flex>
                        </React.Fragment>

                        <Modal
                            open={isModalOpen}
                            onCancel={exitModal}
                            centered
                            footer={
                                <Space>
                                    <Button type="primary" onClick={exitModal}>
                                        Okay
                                    </Button>
                                </Space>
                            }
                            width={800}
                        >
                            <Descriptions 
                                title="User Info" 
                                layout="vertical" 
                                bordered items={descriptionItems} 
                            />
                        </Modal>
                    </React.Fragment>
                ) : (
                    <React.Fragment> 
                        <Voter_HomePage />
                    </React.Fragment>
                )
            }
            
        </React.Fragment>
    )
}

export default Voter_ViewResponses