import { Button, Flex, Result } from "antd"
import Voter_ElectionPage from "./Voter_ElectionPage";
import React, { useState } from "react";

const Voter_GettingStarted: React.FC = () => {

    const [selectedItemMenu, setSelectedItemMenu] = useState<string>('1');

    const handleGetStartedClick = () => {
        setSelectedItemMenu("2");
    };

    return (
        <React.Fragment>
            {
                selectedItemMenu === "1" ? (
                    <Flex justify="center" align="center">
                        <Result
                            status="success"
                            title="Welcome to Your Voting Dashboard!"
                            subTitle="Your Voting Access is Ready. Setting up your voting dashboard will take 1-5 minutes, please wait."
                            extra={[
                                <Button size="large" type="primary" key="console" onClick={handleGetStartedClick}>
                                    Get Started
                                </Button>
                            ]}
                        />
                    </Flex>
                ) : selectedItemMenu === "2" ? (
                    <Voter_ElectionPage />
                ) : null
            }
        </React.Fragment>
        
    )
}

export default Voter_GettingStarted