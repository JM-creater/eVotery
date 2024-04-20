import { Card, Col, Row, Flex, Badge, } from 'antd'
import Title from 'antd/es/typography/Title';
import axios from 'axios';
import React, { useEffect, useState } from 'react'
import noresults from '../../assets/no-results.jpg'

const GET_POSITION_URL = 'https://localhost:7196/Position/get-all';
const GET_CANDIDATERESULT_URL = 'https://localhost:7196/Result/results/';

type PositionType = {
    id: string;
    name: string;
}

type CandidateResultType = {
    candidateId: string;
    candidateName: string;
    candidateImage: string
    voteCount: number;
}

const { Meta } = Card;

const Voter_ResultPage: React.FC = () => {

    const [positions, setPositions] = useState<PositionType[]>([]);
    const [resultsByPosition, setResultsByPosition] = useState<{[key: string]: CandidateResultType[]}>({});

    useEffect(() => {
        const fetchPositions = async () => {
            try {
                const response = await axios.get(GET_POSITION_URL);
                setPositions(response.data);
                response.data.forEach((position: PositionType) => {
                    fetchCandidatesResult(position.id);
                });
            } catch (error) {
                console.error('Error fetching positions:', error);
            }
        }

        const fetchCandidatesResult = async (id: string) => {
            try {
                const response = await axios.get(`${GET_CANDIDATERESULT_URL}${id}`);
                if (response.data && response.data.result) {
                    setResultsByPosition(prev => ({
                        ...prev,
                        [id]: response.data.result instanceof Array ? response.data.result : [response.data.result]
                    }));
                }
            } catch (error) {
                console.error(error);
            }
        };

        fetchPositions();
    }, []);

    return (
        <React.Fragment>
            <Flex justify='center' align='center'>
                <Title>RESULTS FOR ELECTION 2024</Title>   
            </Flex>
            
            <Row gutter={{ xs: 8, sm: 16, md: 24, lg: 32 }}>
                {
                    positions.map(position => (
                        <Col span={8} key={position.id} style={{ marginBottom: 24 }}>
                            <Title level={4}>{position.name}</Title>
                            {
                                resultsByPosition[position.id] && resultsByPosition[position.id].length > 0 ? (
                                    resultsByPosition[position.id].map(candidate => (
                                        <React.Fragment  key={candidate.candidateId}> 
                                            <Badge.Ribbon 
                                                text="Winner"
                                                color="green"
                                            >
                                                <Card
                                                    cover={<img alt="candidate" 
                                                        src={`https://localhost:7196/${candidate.candidateImage}`} 
                                                        style={{ 
                                                            width: '100%',
                                                            height: '100%',
                                                            objectFit: 'cover'
                                                        }} 
                                                    />}
                                                >
                                                    <Meta title={candidate.candidateName} description={`Votes: ${candidate.voteCount}`} />
                                                </Card>
                                            </Badge.Ribbon>
                                        </React.Fragment>
                                        
                                    ))
                                ) : (
                                    <React.Fragment>
                                        <Card
                                            style={{ marginBottom: 16 }}
                                            cover={<img alt="candidate" src={noresults} />}
                                        >
                                            <Meta title="No results available." />
                                        </Card>
                                    </React.Fragment>
                                )
                            }
                        </Col>
                    ))
                }
            </Row>
        </React.Fragment>
        
    )
}

export default Voter_ResultPage