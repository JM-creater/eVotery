import { Button, Flex, Result } from 'antd'
import React from 'react'
import { useNavigate } from 'react-router-dom'
import '../common/Not_FoundPage.css'

const Not_FoundPage: React.FC = () => {

    const navigate = useNavigate();

    const handleBackToLogin = () => {
        navigate('/');
    };

    return (
        <Flex justify='center' align='center' className="no-existpage-container">
            <Result
                status="404"
                title="404"
                subTitle="Sorry, the page you visited does not exist."
                extra={
                    <Button 
                        type="primary" 
                        onClick={handleBackToLogin}
                    >
                        Back Home
                    </Button>
                }
                
            />
        </Flex>
    )
}

export default Not_FoundPage