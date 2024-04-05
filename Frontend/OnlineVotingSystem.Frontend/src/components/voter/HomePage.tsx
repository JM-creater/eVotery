import React, { useState } from 'react';
import { Breadcrumb, Layout, Menu, theme } from 'antd';
import Logo from '../../assets/samples/Logo.png'
import { useNavigate } from 'react-router-dom';
import '../voter/HomePage.css'
import { HomeOutlined } from '@ant-design/icons';

const { Header, Content, Footer } = Layout;


const App: React.FC = () => {

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
                    {selectedItemMenu === '2' && (
                        <React.Fragment>
                            <Breadcrumb.Item>Election</Breadcrumb.Item>
                        </React.Fragment>
                    )}
                    {selectedItemMenu === '3' && (
                        <React.Fragment>
                            <Breadcrumb.Item>Result</Breadcrumb.Item>
                        </React.Fragment>
                    )}
                </Breadcrumb>

                <div
                    style={{
                        background: colorBgContainer,
                        minHeight: 280,
                        padding: 24,
                        borderRadius: borderRadiusLG,
                    }}
                >
                    Content
                </div>
            </Content>
            
            <div className="footer-container">
                <Footer style={{ textAlign: 'center' }}>
                    eVotery ©{new Date().getFullYear()} Created by Joseph Martin Garado
                </Footer>
            </div>
            
        </Layout>
    );
};

export default App;