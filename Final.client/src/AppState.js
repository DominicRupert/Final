import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  profile: {},
  activeProfile: {},
  keeps: [],
  vaults: [],
  vaultKeeps: [],
  profileKeeps: [],
  profileVaults: [],
  profileVaultKeeps: [],
  activeVault: {},
  myVaults: [],

  activeKeep: {},
  activeVaultKeeps: [],
  accountKeeps: [],
  accountVaults: [],



})
