import { Navigate } from 'react-router-dom';
import { useAuth } from  './AuthContext'; 
import { ReactNode } from 'react';

interface Props {
    children?: ReactNode;
}

const PrivateRoute = ({ children }: Props) => {
    const { authToken } = useAuth();

    if (!authToken) {
        return <Navigate to="/" />;
    }

    return children;
};

export default PrivateRoute