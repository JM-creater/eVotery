import { DownloadOutlined } from '@ant-design/icons'
import { Button, Card, Col, Flex, Row, Statistic, StatisticProps } from 'antd'
import React from 'react'
import CountUp from 'react-countup';
import { Bar, BarChart, CartesianGrid, Legend, Tooltip, XAxis, YAxis } from 'recharts';
import '../admin/Admin_Dashboard.css'
import Title from 'antd/es/typography/Title';

const data = [
    {
        "name": "Page A",
        "uv": 4000,
        "pv": 2400
    },
    {
        "name": "Page B",
        "uv": 3000,
        "pv": 1398
    },
    {
        "name": "Page C",
        "uv": 2000,
        "pv": 9800
    },
    {
        "name": "Page D",
        "uv": 2780,
        "pv": 3908
    },
    {
        "name": "Page E",
        "uv": 1890,
        "pv": 4800
    },
    {
        "name": "Page F",
        "uv": 2390,
        "pv": 3800
    },
    {
        "name": "Page G",
        "uv": 3490,
        "pv": 4300
    }
]

const Admin_Dashboard: React.FC = () => {

    const formatter: StatisticProps['formatter'] = (value) => {
        return <CountUp end={value as number} separator="," />;
    };

    return (
        <React.Fragment>
            <div className='stats-container'>   
                <Row gutter={16}>
                    <Col span={6}>
                        <Card title="Election Positions" bordered={false}>
                            <Statistic title="Total Positions" value={112893} formatter={formatter} />
                        </Card>
                    </Col>
                    <Col span={6}>
                        <Card title="Election Candidates" bordered={false}>
                            <Statistic title="Total Candidates" value={112893} formatter={formatter} />
                        </Card>
                    </Col>
                    <Col span={6}>
                        <Card title="Registered Voters" bordered={false}>
                            <Statistic title="Total Registered Voters" value={112893} formatter={formatter} />
                        </Card>
                    </Col>
                    <Col span={6}>
                        <Card title="Voting Participation" bordered={false}>
                            <Statistic title="Voters Participated" value={112893} formatter={formatter} />
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
            

            <Flex justify='center' align='center' wrap='wrap' gap='large'>

                <BarChart width={600} height={300} data={data}>
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="name" />
                    <YAxis />
                    <Tooltip />
                    <Legend />
                    <Bar dataKey="pv" fill="#38B6FF" />
                    <Bar dataKey="uv" fill="#001529" />
                </BarChart>

                <BarChart width={600} height={300} data={data}>
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="name" />
                    <YAxis />
                    <Tooltip />
                    <Legend />
                    <Bar dataKey="pv" fill="#38B6FF" />
                    <Bar dataKey="uv" fill="#001529" />
                </BarChart>

                <BarChart width={600} height={300} data={data}>
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="name" />
                    <YAxis />
                    <Tooltip />
                    <Legend />
                    <Bar dataKey="pv" fill="#38B6FF" />
                    <Bar dataKey="uv" fill="#001529" />
                </BarChart>

                <BarChart width={600} height={300} data={data}>
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="name" />
                    <YAxis />
                    <Tooltip />
                    <Legend />
                    <Bar dataKey="pv" fill="#38B6FF" />
                    <Bar dataKey="uv" fill="#001529" />
                </BarChart>

            </Flex>

        </React.Fragment>
    )
}

export default Admin_Dashboard