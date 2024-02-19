import { createContext, useContext } from 'react';

export interface AuthContextType {
    authToken: string | null;
    login: (token: string) => void;
    setLogout: () => void;
}

export const AuthContext = createContext<AuthContextType>({
    authToken: null,
    login: () => {},
    setLogout: () => {}
});

// eslint-disable-next-line react-refresh/only-export-components
export const useAuth = () => useContext(AuthContext);
