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
        logger.log("[getVaultKeeps]", res.data);
        AppState.keeps = res.data;
    }
    async createVaults(vault){
        const res = await api.post(`api/vaults`, vault);
        logger.log("Vault created", res.data);
        AppState.vaults.unshift(res.data);
    }
    async updateVaults(vault){
        const res = await api.put(`api/vaults/${vault.id}`, vault);
        AppState.activeVault = res.data;
    }
    async deleteVaults(vaultId){
        const res = await api.delete(`api/vaults/${vaultId}`);
        logger.log("Vault deleted", res.data);
        AppState.vaults = res.data;

    }
    async getMyVaults(vaultId){
        const res = await api.get(`account/vaults/`);
        logger.log("[getMyVaults]", res.data);
        AppState.myVaults = res.data;
    }



    
}

export const vaultsService = new VaultsService();