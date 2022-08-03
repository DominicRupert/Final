import { AppState } from "../AppState.js";
import { api } from "./AxiosService.js";

class VaultKeepsService {
  async getVaultKeeps(id) {
    const res = await api.get(`api/vaults/${id}/keeps`);
    AppState.vaultKeeps = res.data;  
  }

    async createVaultKeeps(vaultId, vaultKeep) {
      AppState.vaultKeeps = res.data
        const res = await api.post(`api/vaults/${vaultId}/keeps`, vaultKeep);
        AppState.vaultKeeps.push(res.data);
    }
    async deleteVaultKeeps(vaultKeepId) {
        const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`);
        AppState.vaultKeeps = AppState.vaultKeeps.filter(vaultKeep => vaultKeep.id !== vaultKeepId);
    }
    
  }


export const vaultKeepsService = new VaultKeepsService();