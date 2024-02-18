import React from 'react';
import { Layout, Menu, theme } from 'antd';
import '../admin/Admin_Dashboard.css'
import { useNavigate } from 'react-router-dom';
import Logo from '../../assets/samples/Logo.png'
import { HomeOutlined } from '@ant-design/icons';

const { Header, Content, Footer, Sider } = Layout;

const Admin_Dashboard: React.FC = () => {

  const navigate = useNavigate();

  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  const handleLogout = () => {
    navigate('/');
  };

  return (
    <Layout hasSider>
      <Sider
        style={{ 
          overflow: 'auto', 
          height: '100vh', 
          position: 'fixed', 
          left: 0, 
          top: 0, 
          bottom: 0 
        }}
      >
        <div className="demo-logo-vertical" />

        <Menu theme="dark" mode="inline">
          <div className="image-dashboard-container">
            <img src={Logo} width={80} height={30} />
          </div>

          <Menu.Item key="1"><HomeOutlined /> Dashboard</Menu.Item>
          <Menu.Item key="2">Election</Menu.Item>
          <Menu.Item key="3">Result</Menu.Item>
          <Menu.Item key="4" onClick={handleLogout}>Logout</Menu.Item>
        </Menu>
      </Sider>
      <Layout style={{ marginLeft: 200 }}>
        <Header style={{ padding: 0, background: colorBgContainer }} />
        <Content style={{ margin: '24px 16px 0', overflow: 'initial' }}>
          <div
            style={{
              padding: 24,
              textAlign: 'center',
              background: colorBgContainer,
              borderRadius: borderRadiusLG,
            }}
          >
            <p>long content</p>
            {/* {
              // indicates very long content
              Array.from({ length: 100 }, (_, index) => (
                <React.Fragment key={index}>
                  {index % 20 === 0 && index ? 'more' : '...'}
                  <br />
                </React.Fragment>
              ))
            } */}
          </div>
        </Content>
        <Footer style={{ textAlign: 'center' }}>
          eVotery ©{new Date().getFullYear()} Created by Joseph Martin Garado
        </Footer>
      </Layout>
    </Layout>
  );
};

export default Admin_Dashboard;