import { DownloadOutlined } from '@ant-design/icons'
import { 
    Button, 
    Card, 
    Col, 
    Flex, 
    Row, 
    Statistic, 
    StatisticProps 
} from 'antd'
import React, { useEffect, useState } from 'react'
import CountUp from 'react-countup';
import { 
    Bar, 
    BarChart, 
    CartesianGrid, 
    Legend, 
    Tooltip, 
    XAxis, 
    YAxis 
} from 'recharts';
import '../admin/Admin_Dashboard.css'
import Title from 'antd/es/typography/Title';
import axios from 'axios';

const GET_TOTALCANDIDATES_URL = 'https://localhost:7196/Total/total-candidates';
const GET_TOTALPOSITIONS_URL = 'https://localhost:7196/Total/total-positions';
const GET_TOTALVOTERS_URL = 'https://localhost:7196/Total/total-voters';
const GET_TOTALVOTES_URL = 'https://localhost:7196/Total/total-votes';
const GET_POSITION_URL = 'https://localhost:7196/Position/get-all';
const GET_CANDIDATES_URL = 'https://localhost:7196/Candidate/get-all';

type PositionType = {
    id?: string;
    name?: string;
}

type CandidateType = {
    id?: string;
    firstName?: string;
    lastName?: string;
    positionId?: string;
    votes: VotesType[];
}

type VotesType = {
    id?: string;
    userId?: string;
    candidateId?: string;
}


const Admin_Dashboard: React.FC = () => {

    const [totalCandidates, setTotalCandidates] = useState<number>(0);
    const [totalPositions, setTotalPositions] = useState<number>(0);
    const [totalVoters, setTotalVoters] = useState<number>(0);
    const [totalVotes, setTotalVotes] = useState<number>(0);
    const [position, setPosition] = useState<PositionType[]>([]);
    const [candidate, setCandidate] = useState<CandidateType[]>([]);

    useEffect(() => {
        const fetchTotals = async () => {
            try {
                const candidateResponse = await axios.get(GET_TOTALCANDIDATES_URL);
                setTotalCandidates(candidateResponse.data);

                const positionResponse = await axios.get(GET_TOTALPOSITIONS_URL);
                setTotalPositions(positionResponse.data);

                const voterResponse = await axios.get(GET_TOTALVOTERS_URL);
                setTotalVoters(voterResponse.data);

                const votesResponse = await axios.get(GET_TOTALVOTES_URL);
                setTotalVotes(votesResponse.data);

            } catch (error) {
                console.error(error);
            }
        }

        fetchTotals();
    }, []);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const positionResponse = await axios.get(GET_POSITION_URL);
                setPosition(positionResponse.data);
                const candidateResponse = await axios.get(GET_CANDIDATES_URL);
                setCandidate(candidateResponse.data.map((cand: { votes: string | CandidateType[]; }) => ({
                    ...cand,
                    Votes: cand.votes.length 
                })));
                
            } catch (error) {
                console.error(error);
            }
        }

        fetchData();
    }, []);


    const formatter: StatisticProps['formatter'] = (value) => {
        return <CountUp end={value as number} separator="," />;
    };
    
    return (
        <React.Fragment>
            <div className='stats-container'>   
                <Row gutter={16}>
                    <Col span={6}>
                        <Card title="Election Positions" bordered={false}>
                            <Statistic title="Total Positions" value={totalPositions ?? 0} formatter={formatter} />
                        </Card>
                    </Col>
                    <Col span={6}>
                        <Card title="Election Candidates" bordered={false}>
                            <Statistic title="Total Candidates" value={totalCandidates ?? 0} formatter={formatter} />
                        </Card>
                    </Col>
                    <Col span={6}>
                        <Card title="Registered Voters" bordered={false}>
                            <Statistic title="Total Registered Voters" value={totalVoters ?? 0} formatter={formatter} />
                        </Card>
                    </Col>
                    <Col span={6}>
                        <Card title="Voting Participation" bordered={false}>
                            <Statistic title="Voters Participated" value={totalVotes ?? 0} formatter={formatter} />
                        </Card>
                    </Col>
                </Row>
            </div>

            <Flex justify='space-between' align='center'>
                <div className="title-vote-tally">
                    <Title level={4}>Vote Tally</Title>
                </div>

                <div className="button-download-container">
                    <Button type="default" icon={<DownloadOutlined />} size='middle'>
                        Download
                    </Button>
                </div>
            </Flex>
            
            <div className="chart-grid">
                <React.Fragment>
                    {
                        position.map((pos: PositionType) => {

                            const candidateForPosition = candidate.filter(c => c.positionId === pos.id);
                            const hasVotes = candidateForPosition.some(cand => cand.votes.length > 0);

                            return (
                                <div className='charts-content' key={pos.id}> 
                                    <Title level={5} style={{ textAlign: 'left' }}>{pos.name}</Title>
                                    {
                                        hasVotes ? (
                                            <BarChart width={600} height={300} data={candidateForPosition}>
                                                <CartesianGrid strokeDasharray="3 3" />
                                                <XAxis dataKey="lastName" />
                                                <YAxis 
                                                    allowDecimals={false}
                                                    domain={[0, 'dataMax + 10']}
                                                    allowDataOverflow
                                                />
                                                <Tooltip />
                                                <Legend />
                                                <Bar dataKey="Votes" fill="#38B6FF" />
                                            </BarChart>
                                        ) : (
                                            <React.Fragment>
                                                <div className="no-votes-container">
                                                    <span>No votes yet.</span>
                                                </div>
                                            </React.Fragment>
                                        )
                                    }
                                </div>
                            )
                        })
                    }
                </React.Fragment>
            </div>

        </React.Fragment>
    )
}

export default Admin_Dashboard