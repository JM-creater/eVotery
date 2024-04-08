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
const SUBMITVOTE_URL = ''

const Voter_ElectionPage = () => {

  const [position, setPosition] = useState<PositionType[]>([]);
  const [activeKeys, setActiveKeys] = useState<string[]>([]);

  useEffect(() => {
    const fetchPosition = async () => {
      try {
        const response = await axios.get(GETAll_POSITION_URL);
        setPosition(response.data);

        const positionIds = response.data.map((pos: PositionType) => pos.id);
        setActiveKeys(positionIds);
      } catch (error) {
        console.error(error);
      }
    }

    fetchPosition();
  }, []);

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

      <Collapse items={items} activeKey={activeKeys} />
      
    </React.Fragment>
  )
}

export default Voter_ElectionPage