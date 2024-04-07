import React, { useEffect, useState } from 'react'
import { Checkbox, Collapse, Flex, Typography } from "antd";
import axios from 'axios';

type CandidateType = {
  id?: string;
  firstName?: string;
  lastName?: string;
  image?: string;
  positionId?: string;
}

type PositionType = {
  id?: string;
  name?: string;
  candidates: CandidateType[];
}

const GETAll_POSITION_URL = 'https://localhost:7196/Position/get-all';

const Voter_ElectionPage = () => {

  const [position, setPosition] = useState<PositionType[]>([]);
  const [defaultActiveKey, setDefaultActiveKey] = useState<string[]>([]);

  useEffect(() => {
    const fetchPosition = async () => {
      try {
        const response = await axios.get(GETAll_POSITION_URL);
        setPosition(response.data);
        if (response.data.length > 0) {
          setDefaultActiveKey([response.data[0].id]);
        }
      } catch (error) {
        console.error(error);
      }
    }

    fetchPosition();
  }, []);

  const onChange = (key: string | string[]) => {
    console.log(key);
  };

  const items = position.map(pos => ({
    key: pos.id,
    label: pos.name,
    showArrow: false,
    children: pos.candidates.length > 0 ? pos.candidates.map(cand => (
      <Flex
        key={cand.id}
        justify='flex-start'
        align='center'
        gap='middle'
      >
        <Checkbox />
        <img
          src={`https://localhost:7196/${cand.image}`}
          alt={`${cand.firstName} ${cand.lastName}`}
          width={150}
          style={{ maxWidth: '100%' }}
        />
        <h1>{`${cand.firstName} ${cand.lastName}`}</h1>
      </Flex>
    )) : (
      <Flex justify='center' align='center'>
        <p>No candidates for this position.</p>
      </Flex>
    )
  }));

  return (
    <React.Fragment>

      <Flex justify='center' align='center'>
        <Typography.Title level={1}>
          ELECTION 2024
        </Typography.Title>
      </Flex>

      <Collapse onChange={onChange} items={items} defaultActiveKey={defaultActiveKey} />
      
    </React.Fragment>
  )
}

export default Voter_ElectionPage