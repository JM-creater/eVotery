import React, { useEffect, useState } from 'react'
import '../admin/Admin_Party.css'
import { LoadingOutlined } from '@ant-design/icons';
import { Spin, Table } from 'antd';
import axios from 'axios';

const GET_ALL_PARTY = 'https://localhost:7196/PartyAffiliation/getall-party';
const ADD_PARTY_URL = 'https://localhost:7196/PartyAffiliation/create-party';
const UPDATE_PARTY_URL = 'https://localhost:7196/PartyAffiliation/update-party/';
const DELETE_PARTY_URL = 'https://localhost:7196/PartyAffiliation/delete-party/';


type PartyAffiliationType = {
    id?: string;
    partyName?: string;
    logoImage?: FileType[];
}

type FileType = {
    uid: string;
    name: string;
    status: string;
    originFileObj: File;
}


const Admin_Party: React.FC = () => {

    const[isLoading, setIsLoading] = useState<boolean>(false);
    const[party, setParty] = useState<PartyAffiliationType[]>([]);

    const antIcon = <LoadingOutlined style={{ fontSize: 50 }} spin />

    useEffect(() => {
        const fetchParty = async () => {
            try {
                const response = await axios.get(GET_ALL_PARTY);
                setIsLoading(true);
                setParty(
                    response.data.map(
                        (
                            row: {
                                partyName: PartyAffiliationType;
                                logoImage: PartyAffiliationType;
                            }
                        ) => (
                            {
                                partyName: row.partyName,
                                logoImage: row.logoImage,
                            }
                        )
                    )
                )
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
            }
        }

        fetchParty();
    }, []);

    const columns = [
        {
            title: 'Image',
            dataIndex: 'logoImage',
            key: 'logoImage'
        },
        {
            title: 'Party Name',
            dataIndex: 'partyName',
            key: 'partyName'
        },
        
    ];  

    return (
        <React.Fragment>
            {
                isLoading ? (
                    <div className="loading-container">
                        <div className="loading-content">
                            <Spin indicator={antIcon} />
                        </div>
                    </div>
                ) : (
                    <Table columns={columns} dataSource={party} />
                )
            }
        </React.Fragment>
    )
}

export default Admin_Party