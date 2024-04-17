import { Button, Flex, Result } from "antd"
import Voter_ElectionPage from "./Voter_ElectionPage";
import React, { useEffect, useState } from "react";
import axios from "axios";

const GET_USERBYID_URL = 'https://localhost:7196/User/get-by-id/';

type UserType = {
    id?: string;
    isVoted?: boolean;
}

const Voter_GettingStarted: React.FC = () => {

    const [selectedItemMenu, setSelectedItemMenu] = useState<string>('1');
    const [user, setUser] = useState<UserType | null>(null);

    const handleGetStartedClick = () => {
        setSelectedItemMenu("2");
    };

    useEffect(() => {
        const id = localStorage.getItem('userId');

        const fetchUser = async () => {
            try {
                const response = await axios.get(`${GET_USERBYID_URL}${id}`);
                setUser(response.data);

                if (response.data.isVoted) {
                    setSelectedItemMenu("2");
                }
            } catch (error) {
                console.error(error);
            }
        }

        if (id) {
            fetchUser();
        }
    }, []);

    return (
        <React.Fragment>
            {
                selectedItemMenu === "1" && !user?.isVoted ? (
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