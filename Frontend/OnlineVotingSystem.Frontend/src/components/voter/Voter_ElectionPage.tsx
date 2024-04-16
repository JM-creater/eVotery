import React, { useEffect, useState } from 'react'
import { Button, Checkbox, Collapse, Flex, Typography } from "antd";
import axios from 'axios';
import { toast } from 'react-toastify';
import Voter_VotingComplete from './Voter_VotingComplete';

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

type UserType = {
  id?: string;
  isVoted?: boolean;
}

const GETAll_POSITION_URL = 'https://localhost:7196/Position/get-all';
const SUBMITVOTE_URL = 'https://localhost:7196/Votes/submit-vote';
const GET_USERID_URL = 'https://localhost:7196/User/get-by-id/';

const Voter_ElectionPage: React.FC = () => {

  const [position, setPosition] = useState<PositionType[]>([]);
  const [activeKeys, setActiveKeys] = useState<string[]>([]);
  const [selectedVotes, setSelectedVotes] = useState<{[candidateId: string]: boolean}>({});
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [currentUser, setCurrentUser] = useState<UserType | null>(null);

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

  useEffect(() => {
    const fetchUserId = async () => {
      const userId = localStorage.getItem('userId');

      try {
        const response = await axios.get(`${GET_USERID_URL}${userId}`);
        setCurrentUser(response.data);
      } catch (error) {
        console.error(error);
      }
    }

    fetchUserId();
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
    children: pos.candidates.length > 0 ? (
      <React.Fragment>
        <div className="candidate-select-container">
          {
            pos.candidates.map(cand => (
              <div className='candidate-select-content'> 
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
              </div>
            ))
          }
        </div>
      </React.Fragment>
    ) : (
      <Flex justify='center' align='center'>
        <p>No candidates for this position.</p>
      </Flex>
    )
  }));
  
  

  const handleSubmitVote = async () => {
    setIsLoading(true);

    const hasSelectedVotes = Object.entries(selectedVotes).some(checked => checked);
    if(!hasSelectedVotes) {
      toast.error('Please select at least one candidate to vote for.');
      setIsLoading(false);
      return;
    }

    const userId = localStorage.getItem('userId');

    const votePromises = Object.entries(selectedVotes).filter(([, checked]) => checked).map(async ([candidateId]) => {
      try {
        const response = await axios.post(SUBMITVOTE_URL, {
          UserId: userId,
          CandidateId: candidateId,
        });
        return response.data;
      } catch (error) {
        console.error("Error submitting vote for candidateId:", candidateId, error);
        throw error; 
      }
    });

    try {
      await Promise.all(votePromises);
      toast.success('Vote submitted');
      setSelectedVotes({});
      if (currentUser) {
        setCurrentUser({ ...currentUser, isVoted: true });
      }
    } catch (error) {
      console.error("Error submitting votes:", error);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <React.Fragment>
      {
        !currentUser?.isVoted ? (
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
                loading={isLoading}
              >
                Submit
              </Button>
            </Flex>
          </React.Fragment>
        ) : (
          <React.Fragment>
            <Voter_VotingComplete />
          </React.Fragment>
        )
      }
    </React.Fragment>
  )
}

export default Voter_ElectionPage