import { Navigate } from 'react-router-dom';
import { useAuth } from  './AuthContext'; 
import { ReactNode } from 'react';
// import Not_FoundPage from '../components/common/Not_FoundPage';

interface Props {
    children?: ReactNode;
}

const PrivatePassRoute = ({ children }: Props) => {
    const { authToken } = useAuth();

    if (!authToken) {
        return <Navigate to="*" />;
    }

    return children;
};

export default PrivatePassRoute