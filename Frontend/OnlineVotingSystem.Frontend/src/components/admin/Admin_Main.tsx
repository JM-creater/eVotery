import React, { useEffect, useState } from 'react';
import { Badge, Breadcrumb, Layout, Menu, theme } from 'antd';
import '../admin/Admin_Main.css'
import { useNavigate } from 'react-router-dom';
import Logo from '../../assets/samples/Logo.png'
import { 
  DashboardOutlined, 
  FontColorsOutlined, 
  GroupOutlined, 
  InboxOutlined, 
  LogoutOutlined, 
  UnorderedListOutlined,
  UserOutlined,
  UsergroupAddOutlined, 
} from '@ant-design/icons';
import Admin_Votes from './Admin_Votes';
import Admin_VotersList from './Admin_VotersList';
import Admin_Dashboard from './Admin_Dashboard';
import Admin_Position from './Admin_Position';
import Admin_Candidates from './Admin_Candidates';
import Admin_BallotPosition from './Admin_BallotPosition';
import Admin_ElectionTitle from './Admin_ElectionTitle';
import Admin_Profile from './Admin_Profile';
import Admin_Party from './Admin_Party';
import { useAuth } from '../../utils/AuthContext';
import axios from 'axios';

type AdminType = {
  firstName?: string;
  lastName?: string;
  voterImages?: string;
}

const { Header, Content, Sider } = Layout;

const GETVOTERS_COUNT_URL = 'https://localhost:7196/User/get-voters';

const Admin_Main: React.FC = () => {

  const [selectedItemMenu, setSelectedItemMenu] = useState<string>('1');
  const [admin, setAdmin] = useState<AdminType | null>(null);
  const [countVoters, setCountVoters] = useState<number>(0);
  const navigate = useNavigate();
  const { setLogout } = useAuth();

  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  const handleLogout = () => {
    setLogout();
    navigate('/');
  };

  const handleMenuClick = (menuItem: string) => {
    setSelectedItemMenu(menuItem);
  };

  useEffect(() => {
    const fetchVotersCount = async () => {
      try {
        const response = await axios.get(GETVOTERS_COUNT_URL);
        setCountVoters(response.data);
      } catch (error) {
        console.error(error);
      }
    } 

    fetchVotersCount();
  }, []);

  useEffect(() => {
    const item = localStorage.getItem('result');

    if (item) {
      setAdmin(JSON.parse(item));
    }
    
  }, []);

  const renderComponent = () => {
    switch (selectedItemMenu) {
      case '1':
        return <Admin_Dashboard/>
      case '2':
        return <Admin_Votes/>
      case '3':
        return <Admin_VotersList/>
      case '4':
        return <Admin_Position/>
      case '5':
        return <Admin_Candidates/>
      case '6':
        return <Admin_Party/>
      case '7':
        return <Admin_BallotPosition/>
      case '8':
        return <Admin_ElectionTitle/>
      case '9':
        return <Admin_Profile/>
      default:
        return null;
    }
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

        <Menu
          theme="dark"
          mode="inline"
          onClick={({ key }) => handleMenuClick(key as string)}
        >
          <div className="image-dashboard-container">
            <img src={Logo} width={80} height={30} alt='image-dashboard' />
          </div>

          <Menu.SubMenu key="reports" title="Reports">
            <Menu.Item key="1"><DashboardOutlined /> Dashboard</Menu.Item>
            <Menu.Item key="2"><InboxOutlined /> Votes</Menu.Item>
          </Menu.SubMenu>

          <Menu.SubMenu key="manage" title="Manage">
            <Menu.Item key="3"><GroupOutlined /> Voters <Badge count={countVoters ?? 0}/> </Menu.Item>
            <Menu.Item key="4"><UnorderedListOutlined /> Position</Menu.Item>
            <Menu.Item key="5"><UsergroupAddOutlined /> Candidates</Menu.Item>
            <Menu.Item key="6"><UsergroupAddOutlined/> Party</Menu.Item>
          </Menu.SubMenu>

          <Menu.SubMenu key="settings" title="Settings">
            <Menu.Item key="7"><InboxOutlined /> Ballot</Menu.Item>
            <Menu.Item key="8"><FontColorsOutlined /> Election</Menu.Item>
            <Menu.Item key="9"><UserOutlined /> Profile</Menu.Item>
          </Menu.SubMenu>

          <Menu.Item key="10" onClick={handleLogout} icon={<LogoutOutlined />}>
            Logout
          </Menu.Item>
        </Menu>
      </Sider>

      <Layout style={{ marginLeft: 200 }}>
        <Header style={{ background: '#001529' }}>
          <React.Fragment>
            {
              admin && (
                <div className="user-profile-container">
                  <img 
                    src={`https://localhost:7196/${admin.voterImages}`} 
                    alt="user-profile"
                    className='user-profile-content'
                  />
                  <div className='title-profile-container'>
                    <p>{admin.firstName} {admin.lastName}</p>
                  </div>
                </div>
              )
            }
          </React.Fragment>
          
        </Header>
        <Content style={{ margin: '24px 16px 0', overflow: 'initial' }}>

        <Breadcrumb style={{ margin: '0 0 10px' }}>
          <Breadcrumb.Item><DashboardOutlined /> Dashboard</Breadcrumb.Item>
          {selectedItemMenu === '2' && (
              <React.Fragment>
                  <Breadcrumb.Item>Votes</Breadcrumb.Item>
              </React.Fragment>
          )}
          {selectedItemMenu === '3' && (
              <React.Fragment>
                  <Breadcrumb.Item>Voters</Breadcrumb.Item>
              </React.Fragment>
          )}
          {selectedItemMenu === '4' && (
              <React.Fragment>
                  <Breadcrumb.Item>Position</Breadcrumb.Item>
              </React.Fragment>
          )}
          {selectedItemMenu === '5' && (
              <React.Fragment>
                  <Breadcrumb.Item>Candidates</Breadcrumb.Item>
              </React.Fragment>
          )}
          {selectedItemMenu === '6' && (
              <React.Fragment>
                  <Breadcrumb.Item>Party</Breadcrumb.Item>
              </React.Fragment>
          )}
          {selectedItemMenu === '7' && (
              <React.Fragment>
                  <Breadcrumb.Item>Ballot</Breadcrumb.Item>
              </React.Fragment>
          )}
          {selectedItemMenu === '8' && (
              <React.Fragment>
                  <Breadcrumb.Item>Election</Breadcrumb.Item>
              </React.Fragment>
          )}
          {selectedItemMenu === '9' && (
              <React.Fragment>
                  <Breadcrumb.Item>Profile</Breadcrumb.Item>
              </React.Fragment>
          )}
        </Breadcrumb>

          <div
            style={{
              padding: 24,
              textAlign: 'center',
              background: colorBgContainer,
              borderRadius: borderRadiusLG,
            }}
          >
            {renderComponent()}
          </div>
        </Content>
        {/* <Footer 
          style={{ 
            textAlign: 'center'
          }}
        >
          eVotery ©{new Date().getFullYear()} Created by Joseph Martin Garado
        </Footer> */}
      </Layout>
    </Layout>
  );
};

export default Admin_Main;