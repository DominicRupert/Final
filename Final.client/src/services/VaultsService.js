import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultsService{
    async getVaults(){
        const res = await api.get(`api/vaults`);
        AppState.vaults = res.data;
    }
    async getVaultById(id){
        const res = await api.get(`api/vaults/${id}`);
        AppState.activeVault = res.data;
    }
    async getVaultsByUserId(id){
        const res = await api.get(`api/profiles/${id}/vaults`);
        logger.log("[getVaultsByUserId]", res.data);
        AppState.profileVaults = res.data;
    }

    async getVaultKeeps(id){
        const res = await api.get(`api/vaults/${id}/keeps`);
        AppState.vaultKeeps = res.data;
    }
    async createVaults(vault){
        const res = await api.post(`api/vaults`, vault);
        AppState.vaults.push(res.data);
    }
    async updateVaults(vault){
        const res = await api.put(`api/vaults/${vault.id}`, vault);
        AppState.activeVault = res.data;
    }
    async deleteVaults(id){
        const res = await api.delete(`api/vaults/${id}`);
        AppState.vaults = AppState.vaults.filter(vault => vault.id !== id);
    }
    
}

export const vaultsService = new VaultsService();