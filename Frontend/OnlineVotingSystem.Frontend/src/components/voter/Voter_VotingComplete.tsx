import React, { useState } from 'react'
import './Voter_VotingComplete.css'
import { Button, Flex, Typography } from 'antd'
import Voter_ViewResponses from './Voter_ViewResponses';

const Voter_VotingComplete: React.FC = () => {

    const [selectedMenuItem, setSelectedMenuItem] = useState<number>(1);

    const handleMenuSelected = () => {
        setSelectedMenuItem(2);
    };

    return (
        <React.Fragment>
            {
                selectedMenuItem === 1 ? (
                    <Flex justify='center' align='center' vertical style={{ height: '50vh' }}>
                        <Typography.Title level={1}>Thank you for voting!</Typography.Title>
                        <Typography.Paragraph>Your vote has been successfully submitted.</Typography.Paragraph>
                        <Flex gap='middle'>
                            <Button size='large'>Back to Home</Button>
                            <Button 
                                size='large' 
                                type='primary' 
                                onClick={handleMenuSelected}
                            >
                                View Response
                            </Button>
                        </Flex>
                    </Flex>
                ) : (
                    <Voter_ViewResponses />
                )
            }
        </React.Fragment>
    )
}

export default Voter_VotingComplete