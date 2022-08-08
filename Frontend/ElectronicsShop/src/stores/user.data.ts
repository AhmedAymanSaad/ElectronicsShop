import { defineStore } from "pinia";

export const useUserStore = defineStore({
    id: "user",
    state: () => ({
        auth : null as string | null,
        userId : null as string | null,
        roles: [] as string[],
        tst : "test",
    }),
    getters: {
        getUserType: (state) => {
            return state.roles;
        },
    },
    actions: {
        setAuth(authToken: string | null) {
            this.auth = authToken
        },
        setUserType(roles: []) {
            this.roles= roles
        },
        logOutUser() {
            this.auth = null
            this.roles = []
            this.userId = null
        },
    },
    persist:true
});
