import { EditOutlined, EllipsisOutlined, PlusOutlined, SettingOutlined } from '@ant-design/icons'
import { Avatar, Card, Drawer } from 'antd'
import React, { useState } from 'react'
import '../admin/Admin_Candidates.css'

const { Meta } = Card;

const Admin_Candidates:React.FC = () => {

    const [open, setOpen] = useState<boolean>(false);

    const showDrawer = () => {
        setOpen(true);
    };

    const closeDrawer = () => {
        setOpen(false);
    };

    return (
        <React.Fragment>

            <div className="title-candidate-container">
                <h1>Candidates</h1>
            </div>

            <div className="card-main-candidate">
                
                <div className="add-candidate-container" onClick={showDrawer}>
                    <div className="plus-icon-container">
                        <div className="plus-icon-content">
                            <PlusOutlined style={{ fontSize: '800%'}} />
                        </div>
                        <div className="div-title-container">
                            <h3>Add Candidate</h3>
                        </div>
                    </div>
                </div>

                <Card
                    hoverable
                    style={{ width: 300 }}
                    cover={
                        <img
                            alt="example"
                            src="https://gw.alipayobjects.com/zos/rmsportal/JiqGstEfoWAOHiTxclqi.png"
                        />
                    }
                    actions={[
                        <SettingOutlined key="setting" />,
                        <EditOutlined key="edit" />,
                        <EllipsisOutlined key="ellipsis" />,
                    ]}
                >
                    <Meta
                        avatar={<Avatar src="https://api.dicebear.com/7.x/miniavs/svg?seed=8" />}
                        title="Card title"
                        description="This is the description"
                    />
                </Card>

                <Drawer title="Basic Drawer" onClose={closeDrawer} open={open}>
                    <p>Some contents...</p>
                    <p>Some contents...</p>
                    <p>Some contents...</p>
                </Drawer>
            </div>

            

        </React.Fragment>
    )
}

export default Admin_Candidates