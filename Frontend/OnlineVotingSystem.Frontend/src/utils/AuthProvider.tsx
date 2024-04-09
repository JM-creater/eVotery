import { ReactNode, useState } from 'react';
import { AuthContext } from './AuthContext';

interface Props {
    children?: ReactNode;
}

export const AuthProvider = ({ children }: Props) => {
    const [authToken, setAuthToken] = useState(localStorage.getItem('token'));

    const login = (token: string) => {
        localStorage.setItem('token', token);
        setAuthToken(token);
    };

    const setLogout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('userId');
        localStorage.removeItem('result');
        setAuthToken(null);
    };

    return (
        <AuthContext.Provider value={{ authToken, login, setLogout }}>
            {children}
        </AuthContext.Provider>
    );
};