import { Button, Carousel } from 'antd';
import React, { useState } from 'react';
import Voter_GettingStarted from './Voter_GettingStarted';

const items = [
    {
        key: '1',
        title: 'Secure and Efficient Online Voting',
        content: 'eVotery leverages ASP.NET Core Web API to provide a secure and efficient voting process. Our platform ensures that every vote is counted accurately, offering peace of mind to both administrators and voters.',
    },
    {
        key: '2',
        title: 'Work better together. Schedule meetings',
        content: "Experience a seamless voting process with user-friendly interface, designed for optimal user experience. Whether the administrator setting up the vote or a voter casting your ballot, our system is tailored to your needs.",
    },
    {
        key: '3',
        title: 'The best app to increase your productivity',
        content: 'With eVotery, get real-time updates on voting results. Our cutting-edge technology ensures that the voting data is processed quickly and accurately, allowing for immediate insight into electoral outcomes.',
    },
]

const Voter_HerSection: React.FC = () => {

    const [selectedItemMenu, setSelectedItemMenu] = useState<number>(1);

    const handleSelectedMenu = () => {
        setSelectedItemMenu(2);
    };  

    const handleDemoClick = () => {
        window.open('https://youtu.be/3K2oMqYEGdY', '_blank'); 
    };

    return (
        <React.Fragment>
            {
                selectedItemMenu === 1 ? (
                    <div id="hero" className="heroBlock">
                        <Carousel autoplay>
                            {
                                items.map(item => {
                                    return (
                                        <div key={item.key} className="container-fluid">
                                            <div className="content">
                                                <h3>{item.title}</h3>
                                                <p>{item.content}</p>
                                                <div className="btnHolder">
                                                    <Button type="primary" size="large" onClick={handleSelectedMenu}>Vote Now</Button>
                                                    <Button size="large" onClick={handleDemoClick}> Watch a Video</Button>
                                                </div>
                                            </div>
                                        </div>  
                                    );
                                })
                            }
                        </Carousel>
                    </div>
                ) : (
                    <Voter_GettingStarted />
                )
            }
            
        </React.Fragment>
        
    );
}

export default Voter_HerSection;