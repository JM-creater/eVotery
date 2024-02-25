import { EditOutlined, EllipsisOutlined, PlusOutlined, SettingOutlined } from '@ant-design/icons'
import { Avatar, Button, Card, Col, Drawer, Form, Input, Row, Select, Space, DatePicker} from 'antd'
import React, { useEffect, useState } from 'react'
import '../admin/Admin_Candidates.css'
import { Option } from 'antd/es/mentions';
import axios from 'axios';

const CREATE_CANDIDATE_URL = 'https://localhost:7196/Candidate/create';
const GETALL_CANDIDATE_URL = 'https://localhost:7196/Candidate/get-all';

const { Meta } = Card;

type CandidateType = {
    firstName?: string;
    lastName?: string;
    image?: string;
    ballotId?: string;
    positionId?: string;
}

const Admin_Candidates:React.FC = () => {

    const [candidates, setCandidates] = useState<CandidateType[]>([]);
    const [open, setOpen] = useState<boolean>(false);

    const showDrawer = () => {
        setOpen(true);
    };

    const closeDrawer = () => {
        setOpen(false);
    };

    useEffect(() => {
        const fetchCandidates = async () => {
            try {
                const response = await axios.get(GETALL_CANDIDATE_URL);
                setCandidates(response.data);
            } catch (error) {
                console.error(error);
            }
        }

        fetchCandidates();
    }, []);

    return (
        <React.Fragment>

            <div className="title-candidate-container">
                <h3 className='title-candidate-content'>Candidates</h3>
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

                <Drawer 
                    title="Create a new candidate" 
                    onClose={closeDrawer} 
                    open={open}
                    width={720}
                    styles={{
                        body: {
                            paddingBottom: 80,
                        },
                    }}
                    extra={
                        <Space>
                            <Button 
                                onClick={closeDrawer} 
                                type="primary" 
                                danger
                            >
                                Cancel
                            </Button>
                            <Button 
                                onClick={closeDrawer} 
                                type="primary"
                            >
                                Submit
                            </Button>
                        </Space>
                    }    
                >
                    <Form layout="vertical">

                        <Row gutter={16}>
                            <Col span={12}>
                                <Form.Item
                                    name="name"
                                    label="Name"
                                    rules={[{ required: true, message: 'Please enter user name' }]}
                                >
                                    <Input placeholder="Please enter user name" />
                                </Form.Item>
                            </Col>
                            <Col span={12}>
                                <Form.Item
                                    name="url"
                                    label="Url"
                                    rules={[{ required: true, message: 'Please enter url' }]}
                                >
                                    <Input
                                        style={{ width: '100%' }}
                                        addonBefore="http://"
                                        addonAfter=".com"
                                        placeholder="Please enter url"
                                    />
                                </Form.Item>
                            </Col>
                        </Row>

                        <Row gutter={16}>
                            <Col span={12}>
                            <Form.Item
                                name="owner"
                                label="Owner"
                                rules={[{ required: true, message: 'Please select an owner' }]}
                            >
                                <Select placeholder="Please select an owner">
                                    <Option value="xiao">Xiaoxiao Fu</Option>
                                    <Option value="mao">Maomao Zhou</Option>
                                </Select>
                            </Form.Item>
                            </Col>
                            <Col span={12}>
                            <Form.Item
                                name="type"
                                label="Type"
                                rules={[{ required: true, message: 'Please choose the type' }]}
                            >
                                <Select placeholder="Please choose the type">
                                    <Option value="private">Private</Option>
                                    <Option value="public">Public</Option>
                                </Select>
                            </Form.Item>
                            </Col>
                        </Row>

                        <Row gutter={16}>
                            <Col span={12}>
                            <Form.Item
                                name="approver"
                                label="Approver"
                                rules={[{ required: true, message: 'Please choose the approver' }]}
                            >
                                <Select placeholder="Please choose the approver">
                                    <Option value="jack">Jack Ma</Option>
                                    <Option value="tom">Tom Liu</Option>
                                </Select>
                            </Form.Item>
                            </Col>
                            <Col span={12}>
                                <Form.Item
                                    name="dateTime"
                                    label="DateTime"
                                    rules={[{ required: true, message: 'Please choose the dateTime' }]}
                                >
                                    <DatePicker.RangePicker
                                        style={{ width: '100%' }}
                                        getPopupContainer={(trigger) => trigger.parentElement!}
                                    />
                                </Form.Item>
                            </Col>
                        </Row>

                        <Row gutter={16}>
                            <Col span={24}>
                                <Form.Item
                                    name="description"
                                    label="Description"
                                    rules={[
                                        {
                                            required: true,
                                            message: 'please enter url description',
                                        },
                                    ]}
                                >
                                    <Input.TextArea 
                                        rows={4} 
                                        placeholder="please enter url description" 
                                    />
                                </Form.Item>
                            </Col>
                        </Row>
                    </Form>
                </Drawer>
            </div>

        </React.Fragment>
    )
}

export default Admin_Candidates