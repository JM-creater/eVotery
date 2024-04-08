import React, { useEffect, useState } from 'react'
import { Button, Checkbox, Collapse, Flex, Typography } from "antd";
import axios from 'axios';
import { toast } from 'react-toastify';

type CandidateType = {
  id: string;
  firstName?: string;
  lastName?: string;
  image?: string;
  positionId?: string;
}

type PositionType = {
  id: string;
  name?: string;
  candidates: CandidateType[];
}

const GETAll_POSITION_URL = 'https://localhost:7196/Position/get-all';
const SUBMITVOTE_URL = 'https://localhost:7196/Votes/submit-vote';

const Voter_ElectionPage = () => {

  const [position, setPosition] = useState<PositionType[]>([]);
  const [activeKeys, setActiveKeys] = useState<string[]>([]);
  const [selectedVotes, setSelectedVotes] = useState<{[candidateId: string]: boolean}>({});

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

  const handleVoteChange = (candidateId: string, checked: boolean) => {
    setSelectedVotes({
      ...selectedVotes,
      [candidateId]: checked
    });
  };

  const items = position.map(pos => ({
    key: pos.id,
    label: pos.name,
    showArrow: false,
    children: pos.candidates.length > 0 ? pos.candidates.map(cand => (
      <React.Fragment>
        <Flex
          key={cand.id}
          justify='flex-start'
          align='center'
          gap='middle'
        >
          <Checkbox 
            onChange={(e) => handleVoteChange(cand.id, e.target.checked)}
            checked={!!selectedVotes[cand.id]}
          />
          <img
            src={`https://localhost:7196/${cand.image}`}
            alt={`${cand.firstName} ${cand.lastName}`}
            width={150}
            style={{ maxWidth: '100%' }}
          />
          <h1>{`${cand.firstName} ${cand.lastName}`}</h1>
      </Flex>
      </React.Fragment>
    )) : (
      <React.Fragment>
        <Flex justify='center' align='center'>
          <p>No candidates for this position.</p>
        </Flex>
      </React.Fragment>
    )
  }));

  const handleSubmitVote = async () => {
    const userId = localStorage.getItem('userId');

    const votePromises = Object.entries(selectedVotes).filter(([, checked]) => checked).map(async ([candidateId]) => {
      const response = await axios.post(SUBMITVOTE_URL, {
        UserId: userId,
        CandidateId: candidateId,
      });
      return response.data;
    });

    Promise.all(votePromises).then(() => {
      toast.success('Vote submitted');
      setSelectedVotes({});
    }).catch((error) => {
      console.error(error);
    });
  };

  return (
    <React.Fragment>

      <Flex justify='center' align='center'>
        <Typography.Title level={1}>
          ELECTION 2024
        </Typography.Title>
      </Flex>

      <Collapse items={items} activeKey={activeKeys} />

      <Flex justify='center' align='center'>
        <Button
          size='large'
          style={{  width: '40vh', margin: 30 }}
          type='primary'
          onClick={handleSubmitVote}
        >
          Submit
        </Button>
      </Flex>
      
    </React.Fragment>
  )
}

export default Voter_ElectionPage