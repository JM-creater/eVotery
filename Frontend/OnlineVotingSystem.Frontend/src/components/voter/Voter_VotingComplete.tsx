import React from 'react'
import './Voter_VotingComplete.css'
import { Button, Flex, Typography } from 'antd'

const Voter_VotingComplete: React.FC = () => {
    return (
        <React.Fragment>
            <Flex justify='center' align='center' vertical style={{ height: '50vh' }}>
                <Typography.Title level={1}>Thank you for voting!</Typography.Title>
                <Typography.Paragraph>Your vote has been successfully submitted.</Typography.Paragraph>
                <Flex gap='middle'>
                    <Button size='large'>Home</Button>
                    <Button size='large' type='primary'>View Response</Button>
                </Flex>
            </Flex>
        </React.Fragment>
    )
}

export default Voter_VotingComplete