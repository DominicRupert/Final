<template>
  <form class="row g-3 py-3 text-white">
    <div class="col-md-6">
      <label for="" class="form-label">New Keep Name</label>
      <input
        type="text"
        class="form-control"
        id="keep-name"
        v-model="editable.name"
      />
      <label for="" class="form-label">New Keep description</label>
      <input
        type="text"
        class="form-control"
        id="keep-description"
        v-model="editable.description"
      />
      <label for="" class="form-label">Image Url</label>
      <input
        type="url"
        class="form-control"
        id="keep-img"
        v-model="editable.img"
      />
    </div>

    <div class="col-12">
      <button type="submit" @click.prevent="createKeep" class="btn btn-primary">
        Make new keep
      </button>
    </div>
  </form>
</template>


<script>
import { AppState } from "../AppState";
import { computed, reactive, onMounted, onBeforeUnmount, ref, watchEffect } from "vue";
import { Modal } from "bootstrap";
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/KeepsService.js";
import Pop from "../utils/Pop.js";
export default {
  props: { keep: { type: Object, required: true } },
  setup(props) {

    const editable = ref({})
    watchEffect(() => {
      editable.value = { ...props.keep }
    })

    return {
      editable,
      keep: computed(() => AppState.keeps),

      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
      async createKeep() {
        try {
          await keepsService.createKeep(editable.value)
          Pop.success(`Keep created`)
        } catch (error) {
          logger.error(error);
        }
      },


    };
  },
};
</script>


<style lang="scss" scoped>
</style>