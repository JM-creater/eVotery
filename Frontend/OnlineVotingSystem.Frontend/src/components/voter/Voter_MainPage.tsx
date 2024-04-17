import React, { useState } from 'react';
import { Breadcrumb, Layout, Menu, theme } from 'antd';
// import Logo from '../../assets/samples/Logo.png'
import { useNavigate } from 'react-router-dom';
import '../voter/Voter_MainPage.css'
import { HomeOutlined } from '@ant-design/icons';
import Voter_HomePage from './Voter_HomePage';
import Voter_ResultPage from './Voter_ResultPage';
import Voter_GettingStarted from './Voter_GettingStarted';

const { Header, Content } = Layout;

const Voter_MainPage: React.FC = () => {

    const navigate = useNavigate();
    const [selectedItemMenu, setSelectedItemMenu] = useState<number>(1);

    const {
        token: { colorBgContainer, borderRadiusLG },
    } = theme.useToken();

    const handleLogout = () => {
        navigate('/');
    };

    const handleMenuClick = (menuItem: number) => {
        setSelectedItemMenu(menuItem);
    };

    const menuItems = [
        {
            key: '1',
            label: 'Home',
            onClick: () => handleMenuClick(1),
        },
        {
            key: '2',
            label: 'Election',
            onClick: () => handleMenuClick(2),
        },
        {
            key: '3',
            label: 'Result',
            onClick: () => handleMenuClick(3),
        },
        {
            key: '4',
            label: 'Logout',
            onClick: handleLogout,
        },
    ];

    const renderComponent = () => {
        switch (selectedItemMenu) {
            case 1:
                return <Voter_HomePage/>
            case 2:
                return <Voter_GettingStarted/>
            case 3: 
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
                    items={menuItems}
                />
                {/* <div className="image-home-container">
                    <img src={Logo} width={80} height={30} alt='image-home' />
                </div> */}
            </Header>

            <Content style={{ padding: '0 48px' }}>

            <Breadcrumb
                style={{ margin: '16px 0' }}
                separator=">"
                items={[
                    {
                        onClick: () => handleMenuClick(1),
                        title: (
                            <React.Fragment>
                                <HomeOutlined />
                                <span className="breadcrumb-item">Home</span>
                            </React.Fragment>
                        ),
                    },
                    ...(selectedItemMenu === 2 ? [{
                        onClick: () => handleMenuClick(2),
                        title: <span className="breadcrumb-item">Election</span>,
                    }] : []),
                    ...(selectedItemMenu === 3 ? [{
                        onClick: () => handleMenuClick(3),
                        title: <span className="breadcrumb-item">Result</span>,
                    }] : [])
                ]}
            />

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