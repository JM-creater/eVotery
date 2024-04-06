import React, { useState } from 'react';
import { Breadcrumb, Layout, Menu, theme } from 'antd';
import Logo from '../../assets/samples/Logo.png'
import { useNavigate } from 'react-router-dom';
import '../voter/Voter_MainPage.css'
import { HomeOutlined } from '@ant-design/icons';
import Voter_HomePage from './Voter_HomePage';
import Voter_ResultPage from './Voter_ResultPage';
import Voter_GettingStarted from './Voter_GettingStarted';

const { Header, Content } = Layout;

const Voter_MainPage: React.FC = () => {

    const navigate = useNavigate();
    const [selectedItemMenu, setSelectedItemMenu] = useState<string>('1');

    const {
        token: { colorBgContainer, borderRadiusLG },
    } = theme.useToken();

    const handleLogout = () => {
        navigate('/');
    };

    const handleMenuClick = (menuItem: string) => {
        setSelectedItemMenu(menuItem);
    };

    const renderComponent = () => {
        switch (selectedItemMenu) {
            case '1':
                return <Voter_HomePage/>
            case '2':
                return <Voter_GettingStarted/>
            case '3': 
                return <Voter_ResultPage/>
            default:
                return null;
        }
    };

    return (
        <Layout>

            <Header 
                style={{
                    position: 'sticky',
                    top: 0,
                    zIndex: 1,
                    width: '100%',
                    display: 'flex',
                    alignItems: 'center',
                }}
            >
                <div className="demo-logo" />
                <Menu 
                    theme="dark" 
                    mode="horizontal" 
                    style={{ flex: 1, minWidth: 0 }}
                    onClick={({ key }) => handleMenuClick(key as string)}
                >
                    <div className="image-home-container">
                        <img src={Logo} width={80} height={30} alt='image-home' />
                    </div>

                    <Menu.Item key="1">Home</Menu.Item>
                    <Menu.Item key="2">Election</Menu.Item>
                    <Menu.Item key="3">Result</Menu.Item>
                    <Menu.Item key="4" onClick={handleLogout}>Logout</Menu.Item>
                    
                </Menu>
            </Header>

            <Content style={{ padding: '0 48px' }}>

                <Breadcrumb style={{ margin: '16px 0' }}>
                    <Breadcrumb.Item><HomeOutlined /> Home</Breadcrumb.Item>
                    {
                        selectedItemMenu === '2' && (
                            <React.Fragment>
                                <Breadcrumb.Item>Election</Breadcrumb.Item>
                            </React.Fragment>
                        )
                    }
                    {
                        selectedItemMenu === '3' && (
                            <React.Fragment>
                                <Breadcrumb.Item>Result</Breadcrumb.Item>
                            </React.Fragment>
                        )
                    }
                </Breadcrumb>

                <div
                    style={{
                        background: colorBgContainer,
                        minHeight: 280, 
                        padding: 24,
                        borderRadius: borderRadiusLG,
                    }}
                >
                    {
                        renderComponent()
                    }
                </div>
            </Content>
            
        </Layout>
    );
};

export default Voter_MainPage;