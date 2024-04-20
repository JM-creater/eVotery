import React, { useEffect, useState } from 'react';
import { Badge, Breadcrumb, Layout, Menu, MenuProps, theme } from 'antd';
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

  const [selectedItemMenu, setSelectedItemMenu] = useState<number>(1);
  const [admin, setAdmin] = useState<AdminType | null>(null);
  const [countVoters, setCountVoters] = useState<number>(0);
  const navigate = useNavigate();
  const { setLogout } = useAuth();

  console.log(admin);

  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  const handleLogout = () => {
    setLogout();
    navigate('/');
  };

  const handleMenuClick = (menuItem: number) => {
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

    const handleFocus = () => {
      fetchVotersCount();
    }

    window.addEventListener("focus", handleFocus);
    
    return () => {
      window.removeEventListener("focus", handleFocus)
    }
  }, []);

  useEffect(() => {
    const item = localStorage.getItem('result');

    if (item) {
      setAdmin(JSON.parse(item));
    }
    
  }, []);

  const renderComponent = () => {
    switch (selectedItemMenu) {
      case 1:
        return <Admin_Dashboard/>
      case 2:
        return <Admin_Votes/>
      case 3:
        return <Admin_VotersList/>
      case 4:
        return <Admin_Position/>
      case 5:
        return <Admin_Candidates/>
      case 6:
        return <Admin_Party/>
      case 7:
        return <Admin_BallotPosition/>
      case 8:
        return <Admin_ElectionTitle/>
      case 9:
        return <Admin_Profile/>
      default:
        return null;
    }
  };

  const items: MenuProps['items'] = [
    {
      key: 'reports',
      label: 'Reports',
      icon: <DashboardOutlined />,
      children: [
        { 
          key: '1', 
          label: 'Dashboard', 
          icon: <DashboardOutlined /> 
        },
        { 
          key: '2', 
          label: 'Votes', 
          icon: <InboxOutlined /> 
        },
      ],
    },
    {
      key: 'manage',
      label: 'Manage',
      icon: <GroupOutlined />,
      children: [
        { 
          key: '3', 
          label: (
            <React.Fragment>
              <span>Voters <Badge count={countVoters ?? 0}/></span> 
            </React.Fragment>
          ), 
          icon: <GroupOutlined /> 
        },
        { 
          key: '4', 
          label: 'Position', 
          icon: <UnorderedListOutlined /> 
        },
        { 
          key: '5', 
          label: 'Candidates', 
          icon: <UsergroupAddOutlined /> 
        },
        { 
          key: '6', 
          label: 'Party', 
          icon: <UsergroupAddOutlined /> 
        },
      ],
    },
    {
      key: 'settings',
      label: 'Settings',
      icon: <InboxOutlined />,
      children: [
        { 
          key: '7', 
          label: 'Ballot', 
          icon: <InboxOutlined /> 
        },
        { 
          key: '8', 
          label: 'Election', 
          icon: <FontColorsOutlined /> 
        },
        // { 
        //   key: '9', 
        //   label: 'Profile', 
        //   icon: <UserOutlined /> 
        // },
      ],
    },
    {
      key: '10',
      label: 'Logout',
      icon: <LogoutOutlined />,
      onClick: () => handleLogout(),
    },
  ];

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

        <div className="image-dashboard-container">
          <img src={Logo} width={80} height={30} alt='image-dashboard' />
        </div>

        <Menu
          theme="dark"
          mode="inline"
          onClick={({ key }) => handleMenuClick(parseInt(key))}
          items={items}
        />
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

        <Breadcrumb
          style={{ margin: '0 0 10px' }}
          separator=">"
          items={[
            {
              onClick: () => handleMenuClick(1),
              title: (
                  <React.Fragment>
                      <DashboardOutlined />
                      <span className="breadcrumb-item-admin">Home</span>
                  </React.Fragment>
              ),
            },
            ...(selectedItemMenu === 2 ? [{
              onClick: () => handleMenuClick(2),
              title: <span className="breadcrumb-item-admin">Votes</span>,
            }] : []),
            ...(selectedItemMenu === 3 ? [{
              onClick: () => handleMenuClick(3),
              title: <span className="breadcrumb-item-admin">Voters</span>,
            }] : []),
            ...(selectedItemMenu === 4 ? [{
              onClick: () => handleMenuClick(4),
              title: <span className="breadcrumb-item-admin">Position</span>,
            }] : []),
            ...(selectedItemMenu === 5 ? [{
              onClick: () => handleMenuClick(5),
              title: <span className="breadcrumb-item-admin">Candidates</span>,
            }] : []),
            ...(selectedItemMenu === 6 ? [{
              onClick: () => handleMenuClick(6),
              title: <span className="breadcrumb-item-admin">Party</span>,
            }] : []),
            ...(selectedItemMenu === 7 ? [{
              onClick: () => handleMenuClick(7),
              title: <span className="breadcrumb-item-admin">Ballot</span>,
            }] : []),
            ...(selectedItemMenu === 8 ? [{
              onClick: () => handleMenuClick(8),
              title: <span className="breadcrumb-item-admin">Election</span>,
            }] : []),
            ...(selectedItemMenu === 9 ? [{
              onClick: () => handleMenuClick(9),
              title: <span className="breadcrumb-item-admin">Profile</span>,
            }] : []),
          ]}
        />

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
        
      </Layout>
    </Layout>
  );
};

export default Admin_Main;