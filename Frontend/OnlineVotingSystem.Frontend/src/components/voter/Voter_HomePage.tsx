import { Layout } from 'antd'
import React from 'react'
import Voter_HerSection from './Voter_HeroSection'
import '../voter/Voter_HeroSection.css'

const { Content } = Layout;

const Voter_HomePage: React.FC = () => {
  return (
    <Layout>
      <Content>
        <Voter_HerSection/>
      </Content>
    </Layout>
  )
}

export default Voter_HomePage