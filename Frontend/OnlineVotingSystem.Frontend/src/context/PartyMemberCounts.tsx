import React, { useState, useEffect } from 'react';
import axios from 'axios';

const GET_PARTYMEMBERS_URL = 'https://localhost:7196/PartyAffiliation/get-count-member/'; 

type AsyncPartyMemberCountProps = {
    id: string;
};

const AsyncPartyMemberCount: React.FC<AsyncPartyMemberCountProps> = ({ id }) => {
    const [memberCount, setMemberCount] = useState<string | null>(null);

    useEffect(() => {
        const fetchMemberCount = async () => {
            try {
                const response = await axios.get(`${GET_PARTYMEMBERS_URL}${id}`);
                setMemberCount(response.data.toString());
            } catch (error) {
                console.error(error);
                setMemberCount('Error');
            }
        };

        fetchMemberCount();
    }, [id]);

    return <span>{memberCount === null ? 'Loading...' : memberCount}</span>;
};

export default AsyncPartyMemberCount;
